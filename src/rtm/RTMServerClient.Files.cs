using System;
using System.Collections.Generic;
using System.Text;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        private class SendFileInfo
        {
            public FileTokenType actionType;

            public long from;
            public long xid;
            public HashSet<long> toUids;
            public byte mtype;
            public byte[] fileContent;
            public string filename;
            public string fileExtension;
            
            public string token;
            public string endpoint;
            public int remainTimeout;
            public long lastActionTimestamp;
            public Action<long, int> callback;
        }

        //===========================[ File Token ]=========================//
        //-- Action<token, endpoint, errorCode>
        public void FileToken(Action<string, string, int> callback, long fromUid, FileTokenType tokenType, long targetId, int timeout = 0)
        {
            FileToken(callback, fromUid, tokenType, targetId, null, timeout);
        }

        public void FileToken(Action<string, string, int> callback, long fromUid, FileTokenType tokenType, HashSet<long> targetIds, int timeout = 0)
        {
            FileToken(callback, fromUid, tokenType, 0, targetIds, timeout);
        }

        private void FileToken(Action<string, string, int> callback, long fromUid, FileTokenType tokenType, long targetId = 0, HashSet<long> targetIds = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("filetoken");
            quest.Param("from", fromUid);
            switch (tokenType)
            {
                case FileTokenType.P2P:
                    quest.Param("cmd", "sendfile");
                    quest.Param("to", targetId);
                    break;

                case FileTokenType.Group:
                    quest.Param("cmd", "sendgroupfile");
                    quest.Param("gid", targetId);
                    break;

                case FileTokenType.Room:
                    quest.Param("cmd", "sendroomfile");
                    quest.Param("rid", targetId);
                    break;
                case FileTokenType.Multi:
                    quest.Param("cmd", "sendfiles");
                    quest.Param("tos", targetIds);
                    break;
                case FileTokenType.Broadcast:
                    quest.Param("cmd", "broadcastfile");
                    break;
            }

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string token = "";
                string endpoint = "";
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        token = answer.Want<string>("token");
                        endpoint = answer.Want<string>("endpoint");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(token, endpoint, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(string.Empty, string.Empty, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int FileToken(out string token, out string endpoint, long fromUid, FileTokenType tokenType, long targetId = 0, int timeout = 0)
        {
            return FileToken(out token, out endpoint, fromUid, tokenType, targetId, null, timeout);
        }

        public int FileToken(out string token, out string endpoint, long fromUid, FileTokenType tokenType, HashSet<long> targetIds, int timeout = 0)
        {
            return FileToken(out token, out endpoint, fromUid, tokenType, 0, targetIds, timeout);
        }

        private int FileToken(out string token, out string endpoint, long fromUid, FileTokenType tokenType, long targetId = 0, HashSet<long> targetIds = null, int timeout = 0)
        {
            token = "";
            endpoint = "";

            Quest quest = GenerateQuest("filetoken");
            quest.Param("from", fromUid);
            switch (tokenType)
            {
                case FileTokenType.P2P:
                    quest.Param("cmd", "sendfile");
                    quest.Param("to", targetId);
                    break;

                case FileTokenType.Group:
                    quest.Param("cmd", "sendgroupfile");
                    quest.Param("gid", targetId);
                    break;

                case FileTokenType.Room:
                    quest.Param("cmd", "sendroomfile");
                    quest.Param("rid", targetId);
                    break;
                case FileTokenType.Multi:
                    quest.Param("cmd", "sendfiles");
                    quest.Param("tos", targetIds);
                    break;
                case FileTokenType.Broadcast:
                    quest.Param("cmd", "broadcastfile");
                    break;
            }

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                token = answer.Want<string>("token");
                endpoint = answer.Want<string>("endpoint");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        //===========================[ File Utilies ]=========================//
        private void UpdateTimeout(ref int timeout, ref long lastActionTimestamp)
        {
            long currMsec = ClientEngine.GetCurrentMilliseconds();

            timeout -= (int)((currMsec - lastActionTimestamp) / 1000);

            lastActionTimestamp = currMsec;
        }

        private string ExtraFileExtension(string filename)
        {
            int idx = filename.LastIndexOf('.');
            if (idx == -1)
                return null;

            return filename.Substring(idx + 1);
        }

        private string BuildFileAttrs(SendFileInfo info)
        {
            string fileMD5 = GetMD5(info.fileContent, false);
            string sign = GetMD5(fileMD5 + ":" + info.token, false);

            StringBuilder sb = new StringBuilder();
            sb.Append("{\"sign\":\"");
            sb.Append(sign);
            sb.Append("\"");

            if (info.filename != null && info.filename.Length > 0)
            {
                sb.Append(",\"filename\":\"");
                sb.Append(info.filename);
                sb.Append("\"");

                if (info.fileExtension == null || info.fileExtension.Length == 0)
                {
                    info.fileExtension = ExtraFileExtension(info.filename);
                }
            }

            if (info.fileExtension != null && info.fileExtension.Length > 0)
            {
                sb.Append(",\"ext\":\"");
                sb.Append(info.fileExtension);
                sb.Append("\"");
            }

            sb.Append("}");

            return sb.ToString();
        }

        private Quest BuildSendFileQuest(SendFileInfo info)
        {
            Quest quest = null;
            switch (info.actionType)
            {
                case FileTokenType.P2P:
                    quest = new Quest("sendfile");
                    quest.Param("to", info.xid);
                    break;

                case FileTokenType.Group:
                    quest = new Quest("sendgroupfile");
                    quest.Param("gid", info.xid);
                    break;

                case FileTokenType.Room:
                    quest = new Quest("sendroomfile");
                    quest.Param("rid", info.xid);
                    break;
                case FileTokenType.Multi:
                    quest = new Quest("sendfiles");
                    quest.Param("tos", info.toUids);
                    break;
                case FileTokenType.Broadcast:
                    quest = new Quest("broadcastfile");
                    break;
            }

            quest.Param("pid", projectId);
            quest.Param("from", info.from);
            quest.Param("token", info.token);
            quest.Param("mtype", info.mtype);
            quest.Param("mid", MidGenerator.Gen());

            quest.Param("file", info.fileContent);
            quest.Param("attrs", BuildFileAttrs(info));

            return quest;
        }

        private int SendFileWithClient(SendFileInfo info, TCPClient client)
        {
            UpdateTimeout(ref info.remainTimeout, ref info.lastActionTimestamp);
            if (info.remainTimeout <= 0)
                return fpnn.ErrorCode.FPNN_EC_CORE_TIMEOUT;

            Quest quest = BuildSendFileQuest(info);
            bool success = client.SendQuest(quest, (Answer answer, int errorCode) => {

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        long mtime = answer.Want<long>("mtime");
                        info.callback(mtime, fpnn.ErrorCode.FPNN_EC_OK);

                        ActiveFileGateClient(info.endpoint, client);
                        return;
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }

                info.callback(0, errorCode);
            }, info.remainTimeout);

            if (success)
                return fpnn.ErrorCode.FPNN_EC_OK;
            else
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION;
        }

        private int SendFileWithoutClient(SendFileInfo info, bool originalEndpoint)
        {
            string fileGateEndpoint;
            if (originalEndpoint)
                fileGateEndpoint = info.endpoint;
            else
            {
                if (ConvertIPv4EndpointToIPv6IPPort(info.endpoint, out string ipv6, out int port))
                {
                    fileGateEndpoint = ipv6 + ":" + port;
                }
                else
                {
                    return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION;
                }
            }

            TCPClient client = TCPClient.Create(fileGateEndpoint, true);
            if (errorRecorder != null)
                client.SetErrorRecorder(errorRecorder);

            client.SetConnectionConnectedDelegate((Int64 connectionId, string endpoint, bool connected) => {
                int errorCode = fpnn.ErrorCode.FPNN_EC_OK;

                if (connected)
                {
                    ActiveFileGateClient(info.endpoint, client);
                    errorCode = SendFileWithClient(info, client);
                }
                else if (originalEndpoint)
                {
                    errorCode = SendFileWithoutClient(info, false);
                }
                else
                {
                    errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION;
                }

                if (errorCode != fpnn.ErrorCode.FPNN_EC_OK)
                    info.callback(0, errorCode);
            });

            client.AsyncConnect();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        private void GetFileTokenCallback(SendFileInfo info, string token, string endpoint, int errorCode)
        {
            if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
            {
                info.token = token;
                info.endpoint = endpoint;

                TCPClient fileClient = FecthFileGateClient(info.endpoint);
                if (fileClient != null)
                    errorCode = SendFileWithClient(info, fileClient);
                else
                    errorCode = SendFileWithoutClient(info, true);

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                    return;
            }
            else
                info.callback(0, errorCode);
        }

        //===========================[ Real Send File ]=========================//
        private void RealSendFile(Action<long, int> callback, FileTokenType tokenType, long fromUid, long targetId, HashSet<long> targetIds, byte mtype, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            SendFileInfo info = new SendFileInfo
            {
                from = fromUid,
                actionType = tokenType,
                xid = targetId,
                toUids = targetIds,
                mtype = mtype,
                fileContent = fileContent,
                filename = filename,
                fileExtension = fileExtension,
                remainTimeout = timeout,
                lastActionTimestamp = ClientEngine.GetCurrentMilliseconds(),
                callback = callback
            };

            if (tokenType == FileTokenType.Multi) {
                FileToken((string token, string endpoint, int errorCode) => {
                    GetFileTokenCallback(info, token, endpoint, errorCode);
                }, fromUid, tokenType, info.toUids, timeout);
            } else {
                FileToken((string token, string endpoint, int errorCode) => {
                    GetFileTokenCallback(info, token, endpoint, errorCode);
                }, fromUid, tokenType, info.xid, timeout);
            }
        }

        private int RealSendFile(out long mtime, FileTokenType tokenType, long fromUid, long targetId, HashSet<long> targetIds, byte mtype, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            mtime = 0;

            long lastActionTimestamp = ClientEngine.GetCurrentMilliseconds();
            int errorCode = fpnn.ErrorCode.FPNN_EC_OK;

            string token;
            string endpoint;
            if (tokenType == FileTokenType.Multi) {
                errorCode = FileToken(out token, out endpoint, fromUid, tokenType, targetIds, timeout);
            } else {
                errorCode = FileToken(out token, out endpoint, fromUid, tokenType, targetId, timeout);
            }

            if (errorCode != fpnn.ErrorCode.FPNN_EC_OK)
                return errorCode;

            UpdateTimeout(ref timeout, ref lastActionTimestamp);
            if (timeout <= 0)
                return fpnn.ErrorCode.FPNN_EC_CORE_TIMEOUT;

            TCPClient fileClient = FecthFileGateClient(endpoint);
            if (fileClient == null)
            {
                fileClient = TCPClient.Create(endpoint, true);
                if (fileClient.SyncConnect())
                {
                    ActiveFileGateClient(endpoint, fileClient);
                }
                else
                {
                    UpdateTimeout(ref timeout, ref lastActionTimestamp);
                    if (timeout <= 0)
                        return fpnn.ErrorCode.FPNN_EC_CORE_TIMEOUT;

                    if (ConvertIPv4EndpointToIPv6IPPort(endpoint, out string ipv6, out int port))
                    {
                        fileClient = TCPClient.Create(ipv6 + ":" + port, true);
                        if (fileClient.SyncConnect())
                        {
                            ActiveFileGateClient(endpoint, fileClient);
                        }
                        else
                        {
                            return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION;
                        }
                    }
                    else
                    {
                        return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION;
                    }
                }
            }

            UpdateTimeout(ref timeout, ref lastActionTimestamp);
            if (timeout <= 0)
                return fpnn.ErrorCode.FPNN_EC_CORE_TIMEOUT;

            SendFileInfo info = new SendFileInfo
            {
                from = fromUid,
                toUids = targetIds,
                actionType = tokenType,
                xid = targetId,
                mtype = mtype,
                fileContent = fileContent,
                filename = filename,
                fileExtension = fileExtension,
                token = token,
            };

            Quest quest = BuildSendFileQuest(info);
            Answer answer = fileClient.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            ActiveFileGateClient(endpoint, fileClient);

            mtime = answer.Want<long>("mtime");
            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void SendFile(Action<long, int> callback, long fromUid, long peerUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            RealSendFile(callback, FileTokenType.P2P, fromUid, peerUid, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public int SendFile(out long mtime, long fromUid, long peerUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            return RealSendFile(out mtime, FileTokenType.P2P, fromUid, peerUid, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public void SendFiles(Action<long, int> callback, long fromUid, HashSet<long> peerUids, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            RealSendFile(callback, FileTokenType.Multi, fromUid, 0, peerUids, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public int SendFiles(out long mtime, long fromUid, HashSet<long> peerUids, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            return RealSendFile(out mtime, FileTokenType.Multi, fromUid, 0, peerUids, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public void SendGroupFile(Action<long, int> callback, long fromUid, long groupId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            RealSendFile(callback, FileTokenType.Group, fromUid, groupId, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public int SendGroupFile(out long mtime, long fromUid, long groupId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            return RealSendFile(out mtime, FileTokenType.Group, fromUid, groupId, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public void SendRoomFile(Action<long, int> callback, long fromUid, long roomId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            RealSendFile(callback, FileTokenType.Room, fromUid, roomId, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public int SendRoomFile(out long mtime, long fromUid, long roomId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            return RealSendFile(out mtime, FileTokenType.Room, fromUid, roomId, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public void BroadcastFile(Action<long, int> callback, long fromUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            RealSendFile(callback, FileTokenType.Broadcast, fromUid, 0, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }

        public int BroadcastFile(out long mtime, long fromUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
        {
            return RealSendFile(out mtime, FileTokenType.Broadcast, fromUid, 0, null, (byte)type, fileContent, filename, fileExtension, timeout);
        }
    }
}