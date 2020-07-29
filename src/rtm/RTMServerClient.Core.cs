using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public class RegressiveStrategy
        {
            public int connectFailedMaxIntervalMilliseconds = 1500;
            public int startConnectFailedCount = 5;  
            public int firstIntervalSeconds = 2;
            public int maxIntervalSeconds = 120;
            public int linearRegressiveCount = 5;
        }

        private class RegressiveStatus
        {
            public int connectFailedCount;
            public int regressiveConnectInterval;
        }

        private class ListenStatusInfo
        {
            public bool allP2P = false;
            public bool allGroups = false;
            public bool allRooms = false;
            public bool allEvents = false;
            public HashSet<long> userIds = new HashSet<long>();
            public HashSet<long> groupIds = new HashSet<long>();
            public HashSet<long> roomIds = new HashSet<long>();
            public HashSet<string> events = new HashSet<string>();
        }

        private object interLocker;
        private int projectId;
        private string secretKey;
        private string endpoint;
        private RTMMasterProcessor processor;
        private TCPClient client;
        private bool requireClose; 
        private bool isReconnect;
        private bool autoReconnect;
        private bool canReconnect;
        private RTMConnectionConnectedDelegate connectedDelegate;
        private RTMConnectionCloseDelegate closeDelegate;
        private RegressiveStrategy regressiveStrategy;
        private RegressiveStatus regressiveStatus;
        private common.ErrorRecorder errorRecorder;
        private long lastConnectTime;
        private long lastCloseTime;
        private ListenStatusInfo listenStatusInfo;
        private Dictionary<string, Dictionary<TCPClient, long>> fileClients = new Dictionary<string, Dictionary<TCPClient, long>>();
        private volatile bool routineInited;
        private volatile bool routineRunning;
        private Thread routineThread;

        
        public RTMServerClient(int projectId, string secretKey, string endpoint)
        {
            interLocker = new object();
            autoReconnect = true;
            requireClose = false;
            routineInited = false;
            regressiveStrategy = new RegressiveStrategy();
            regressiveStatus = new RegressiveStatus();
            regressiveStatus.connectFailedCount = 0;
            regressiveStatus.regressiveConnectInterval = regressiveStrategy.firstIntervalSeconds;
            isReconnect = false;
            canReconnect = false;
            this.projectId = projectId;
            this.secretKey = secretKey;
            this.endpoint = endpoint;
            listenStatusInfo = new ListenStatusInfo();
            processor = new RTMMasterProcessor();
            client = TCPClient.Create(endpoint, true);
            ConfigClient();
        }

        private void ConfigClient()
        {
            client.SetConnectionConnectedDelegate((Int64 connectionId, string endpoint, bool connected) =>
            {
                lock(interLocker) {
                    if (connected) {
                        canReconnect = true;
                        lastConnectTime = ClientEngine.GetCurrentMilliseconds();
                    }

                    if (connected && isReconnect) {
                        ListenStatusRestoration();
                    }

                    if (connectedDelegate != null)
                        connectedDelegate(connectionId, endpoint, connected, isReconnect);

                    if (!connected && canReconnect)
                        TryReconnect();
                }
            });

            client.SetConnectionCloseDelegate((Int64 connectionId, string endpoint, bool causedByError) =>
            {
                lock(interLocker) {
                    lastCloseTime = ClientEngine.GetCurrentMilliseconds();
                    if (closeDelegate != null)
                        closeDelegate(connectionId, endpoint, causedByError, isReconnect);

                    if (!requireClose && autoReconnect) {
                        isReconnect = true;
                        if (lastCloseTime - lastConnectTime > regressiveStrategy.connectFailedMaxIntervalMilliseconds) {
                            regressiveStatus.connectFailedCount = 0;
                            regressiveStatus.regressiveConnectInterval = regressiveStrategy.firstIntervalSeconds;
                        }
                        TryReconnect();
                    }
                }
            });
            client.SetQuestProcessor(processor);
            client.ConnectTimeout = RTMServerConfig.globalConnectTimeoutSeconds;
            client.QuestTimeout = RTMServerConfig.globalQuestTimeoutSeconds;
            errorRecorder = RTMServerConfig.errorRecorder;
            if (errorRecorder != null)
                client.SetErrorRecorder(errorRecorder);
        }

        private void TryReconnect()
        {
            if (regressiveStatus.connectFailedCount < regressiveStrategy.startConnectFailedCount) {
                regressiveStatus.connectFailedCount++;
                client.AsyncReconnect();
            } else {
                ClientEngine.RunTask(() => {
                    int sleepInterval = 0;
                    lock(interLocker) {
                        sleepInterval = 1000 * regressiveStatus.regressiveConnectInterval;
                        regressiveStatus.regressiveConnectInterval += (regressiveStrategy.maxIntervalSeconds - regressiveStrategy.firstIntervalSeconds) / regressiveStrategy.linearRegressiveCount;
                    }
                    Thread.Sleep(sleepInterval);
                    lock(interLocker) {
                        regressiveStatus.connectFailedCount++;
                        client.AsyncReconnect();
                    }
                });
            }
        }

        public void SetConnectTimeout(int seconds)
        {
            client.ConnectTimeout = seconds;
        }

        public void SetQuestTimeout(int seconds)
        {
            client.QuestTimeout = seconds;
        }

        public void SetServerPushMonitor(RTMQuestProcessor questProcessor)
        {
            processor.SetProcessor(questProcessor);
            processor.SetErrorRecorder(errorRecorder);
        }

        public void SetRegressiveConnectStrategy(RegressiveStrategy strategy)
        {
            regressiveStrategy = strategy;
        }

        public void SetConnectionConnectedDelegate(RTMConnectionConnectedDelegate ccd)
        {
            lock(interLocker) {
                connectedDelegate = ccd;
            }
        }

        public void SetConnectionCloseDelegate(RTMConnectionCloseDelegate cwcd)
        {
            lock(interLocker) {
                closeDelegate = cwcd;
            }
        }

        public void SetAutoConnect(bool autoConnect)
        {
            client.AutoConnect = autoConnect;
        }

        public void SetAudoReconnect(bool autoReconnect)
        {
            lock(interLocker) {
                this.autoReconnect = autoReconnect;
            }
        }

        public void SetErrorRecorder(common.ErrorRecorder recorder)
        {
            lock(interLocker) {
                errorRecorder = recorder;
                client.SetErrorRecorder(errorRecorder);
                processor.SetErrorRecorder(errorRecorder);
            }
        }

        public bool IsConnected()
        {
            return client.IsConnected();
        }

        public void AsyncConnect()
        {
            client.AsyncConnect();
        }

        public bool SyncConnect()
        {
            return client.SyncConnect();
        }

        public void AsyncReconnect()
        {
            client.AsyncConnect();
        }

        public bool SyncReconnect()
        {
            return client.SyncConnect();
        }

        public void Close()
        {
            lock(interLocker) {
                requireClose = true;
            }
            client.Close();
        }

        private string ConvertIPv4ToIPv6(string ipv4)
        {
            string[] parts = ipv4.Split(new Char[] { '.' });
            if (parts.Length != 4)
                return string.Empty;

            foreach (string part in parts)
            {
                int partInt = Int32.Parse(part);
                if (partInt > 255 || partInt < 0)
                    return string.Empty;
            }

            string part7 = Convert.ToString(Int32.Parse(parts[0]) * 256 + Int32.Parse(parts[1]), 16);
            string part8 = Convert.ToString(Int32.Parse(parts[2]) * 256 + Int32.Parse(parts[3]), 16);
            return "64:ff9b::" + part7 + ":" + part8;
        }

        private bool ConvertIPv4EndpointToIPv6IPPort(string ipv4endpoint, out string ipv6, out int port)
        {
            int idx = ipv4endpoint.LastIndexOf(':');
            if (idx == -1)
            {
                ipv6 = string.Empty;
                port = 0;

                return false;
            }

            string ipv4 = ipv4endpoint.Substring(0, idx);
            string portString = ipv4endpoint.Substring(idx + 1);
            port = Convert.ToInt32(portString, 10);

            ipv6 = ConvertIPv4ToIPv6(ipv4);
            if (ipv6.Length == 0)
                return false;

            return true;
        }

        private string GetMD5(string str, bool upper)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(str);
            return GetMD5(inputBytes, upper);
        }

        private string GetMD5(byte[] bytes, bool upper)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(bytes);
            string f = "x2";

            if (upper)
            {
                f = "X2";
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString(f));
            }

            return sb.ToString();
        }

        private long GenerateSalt()
        {
            return MidGenerator.Gen();
        }

        private Quest GenerateQuest(string cmd)
        {
            long ts = ClientEngine.GetCurrentSeconds();
            long salt = GenerateSalt();
            string sign = GetMD5(projectId + ":" + secretKey + ":" + salt + ":" + cmd + ":" + ts, true);
            Quest quest = new Quest(cmd);
            quest.Param("pid", projectId);
            quest.Param("salt", salt);
            quest.Param("sign", sign);
            quest.Param("ts", ts);
            return quest;
        }

        //===========================[ File Gate Client Functions ]=========================//
        internal void ActiveFileGateClient(string endpoint, TCPClient client)
        {
            lock (interLocker)
            {
                if (fileClients.TryGetValue(endpoint, out Dictionary<TCPClient, long> clients))
                {
                    if (clients.ContainsKey(client))
                        clients[client] = ClientEngine.GetCurrentSeconds();
                    else
                        clients.Add(client, ClientEngine.GetCurrentSeconds());
                }
                else
                {
                    clients = new Dictionary<TCPClient, long>
                    {
                        { client, ClientEngine.GetCurrentSeconds() }
                    };
                    fileClients.Add(endpoint, clients);
                }
            }
        }

        internal TCPClient FecthFileGateClient(string endpoint)
        {
            lock (interLocker)
            {
                if (fileClients.TryGetValue(endpoint, out Dictionary<TCPClient, long> clients))
                {
                    foreach (KeyValuePair<TCPClient, long> kvp in clients)
                        return kvp.Key;
                }
            }

            return null;
        }

        private void CheckFileGateClients()
        {
            HashSet<string> emptyEndpoints = new HashSet<string>();

            lock (interLocker)
            {
                long threshold = ClientEngine.GetCurrentSeconds() - RTMServerConfig.fileGateClientHoldingSeconds;

                foreach (KeyValuePair<string, Dictionary<TCPClient, long>> kvp in fileClients)
                {
                    HashSet<TCPClient> unactivedClients = new HashSet<TCPClient>();

                    foreach (KeyValuePair<TCPClient, long> subKvp in kvp.Value)
                    {
                        if (subKvp.Value <= threshold)
                            unactivedClients.Add(subKvp.Key);
                    }

                    foreach (TCPClient client in unactivedClients)
                    {
                        kvp.Value.Remove(client);
                    }

                    if (kvp.Value.Count == 0)
                        emptyEndpoints.Add(kvp.Key);
                }

                foreach (string endpoint in emptyEndpoints)
                    fileClients.Remove(endpoint);
            }
        }

        private void CheckRoutineInit()
        {
            if (routineInited)
                return;

            lock (interLocker)
            {
                if (routineInited)
                    return;

                routineRunning = true;

                routineThread = new Thread(RoutineFunc)
                {
                    Name = "RTM.ServerClient.RoutineThread",
                    IsBackground = true
                };
                routineThread.Start();


                routineInited = true;
            }
        }

        private void RoutineFunc()
        {
            while (routineRunning)
            {
                Thread.Sleep(1000);

                CheckFileGateClients();
            }
        }

    }
}