using System;
using System.Collections;
using System.Threading;
using com.fpnn;
using com.rtm;

using MessagePack;

namespace demo.rtm
{
    class Program
    {

        static void PrintHashTable(string flag, Hashtable mp)
        {
            Console.WriteLine(flag);
            Console.WriteLine(MessagePackSerializer.ToJson(MessagePackSerializer.Serialize<Hashtable>(mp)));
        }

        static void Main(string[] args)
        {
            RTMServerClient client = new RTMServerClient(11000001, "ef3617e5-e886-4a4e-9eef-7263c0320628", "52.83.245.22", 13315, true, 5000);

            client.AddPushListener(RTMConfig.SERVER_PUSH.recvMessage, (data) =>
            {
                PrintHashTable(RTMConfig.SERVER_PUSH.recvMessage, data);
            });

            client.AddPushListener(RTMConfig.SERVER_PUSH.recvEvent, (data) =>
            {
                PrintHashTable(RTMConfig.SERVER_PUSH.recvEvent, data);
            });

            client.AddPushListener(RTMConfig.SERVER_PUSH.recvPing, (data) =>
            {
                PrintHashTable(RTMConfig.SERVER_PUSH.recvPing, data);
            });

            client.ConnectedCallback = delegate()
            {
                Console.WriteLine("connected");

                client.AddListen(new long[]{123}, new long[123], true, new string[]{"login"}, 5000, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("AddListen return", data);
                    } 
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.SendMessage(1, 123, 2, "a msg", "a attrs", 0, 0, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("SendMessage return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.AddFriends(123, new long[]{1,2,3}, 0, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("AddFriends return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.GetFriends(123, 0, (data, exception) =>
                {
                    if (exception == null)
                    {
                        //long[] uids = Array.ConvertAll<object, long>((object[])data["uids"], (x) => Convert.ToInt64(x));
                        PrintHashTable("GetFriends return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.GetToken(123, 0, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("GetToken return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.AddGroupMembers(123, new long[]{123,1,2,3}, 0, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("AddGroupMembers return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.GetGroupMessage(123, true, 10, 0, 0, 0, 0, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("GetGroupMessage return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.SendFile(111, 123, 50, client.LoadFile("/Users/zhaojianjun/Downloads/20181012043639-A89422AECB0DD16FF82364631290C413.gz"), 0, 10000, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("SendFile return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });

                client.SendFiles(111, new long[2]{123, 456}, 50, client.LoadFile("/Users/zhaojianjun/Downloads/20181012043639-A89422AECB0DD16FF82364631290C413.gz"), 0, 10000, (data, exception) =>
                {
                    if (exception == null)
                    {
                        PrintHashTable("SendFiles return", data);
                    }
                    else
                    {
                        Console.WriteLine(exception.Message);
                    }
                });
            };

            client.ClosedCallback = delegate()
            {
                Console.WriteLine("closed");
            };

            client.ErrorCallback = delegate(Exception e)
            {
                Console.WriteLine("error:");
                Console.WriteLine(e.Message);
            };

            client.Connect();

            while (true)
                Thread.Sleep(10000);

        }
    }
}
