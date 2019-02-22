using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.rtm 
{
        class Test
        {
                static async Task  Main(string[] args)
                {

                    RTMServerClient client = new RTMServerClient(11000001, "ef3617e5-e886-4a4e-9eef-7263c0320628", "52.83.245.22:13315");
					
					try {
						Console.WriteLine(await client.sendMessage(123, 888, 2, "aaa", "bbb"));
						Console.WriteLine(await client.sendMessages(123, new long[] { 3, 4, 5, 888 }, 1, "aaa", "bbb"));
						Console.WriteLine(await client.sendGroupMessage(123, 222, 2, "xxx", "yyy"));
						Console.WriteLine(await client.sendRoomMessage(123, 999, 1, "xxx", "yyy"));
						Console.WriteLine(await client.broadcastMessage(123, 1, "xxx", "yyy"));
						Console.WriteLine(await client.addFriends(888, new long[] { 777,6,4 }));
						Console.WriteLine(await client.deleteFriends(888, new long[] { 4 }));
						Console.WriteLine(await client.getFriends(888));
						Console.WriteLine(await client.isFriend(888, 777));
						Console.WriteLine(await client.isFriends(888, new long[] {777,55,22}));
						Console.WriteLine(await client.addGroupMembers(123, new long[] {777,55,22}));
						Console.WriteLine(await client.deleteGroupMembers(123, new long[] {55,22}));
						Console.WriteLine(await client.deleteGroup(1233));
						Console.WriteLine(await client.getGroupMembers(123));
						Console.WriteLine(await client.isGroupMember(123, 777));
						Console.WriteLine(await client.getUserGroups(777));
						Console.WriteLine(await client.getToken(777));
						Console.WriteLine(await client.getOnlineUsers(new long[] {111, 222, 999}));
						Console.WriteLine(await client.addGroupBan(123, 999, 30));
						Console.WriteLine(await client.removeGroupBan(123, 999));
						Console.WriteLine(await client.addRoomBan(333, 999, 30));
						Console.WriteLine(await client.removeRoomBan(333, 999));
						Console.WriteLine(await client.addProjectBlack(999, 60));
						Console.WriteLine(await client.removeProjectBlack(999));
						Console.WriteLine(await client.isBanOfGroup(123, 999));
						Console.WriteLine(await client.isProjectBlack(999));
						Console.WriteLine(await client.getGroupMessage(123, 100, true));
						Console.WriteLine(await client.getRoomMessage(123, 100, true));
						Console.WriteLine(await client.getBroadcastMessage(100, true));
						Console.WriteLine(await client.getP2PMessage(123, 888, 100, true));
						Console.WriteLine(await client.addRoomMember(123, 888));
						Console.WriteLine(await client.deleteRoomMember(123, 888));
						Console.WriteLine(await client.deleteMessage(123, 888, 333, 2));
						Console.WriteLine(await client.kickout(999));	


					{
                        Dictionary<string, dynamic> msg = await client.getP2PMessage(123, 888, 100, true);
                        Console.WriteLine((int)msg["num"]);
                        Console.WriteLine((long)msg["lastid"]);
                        Console.WriteLine((long)msg["begin"]);
                        Console.WriteLine((long)msg["end"]);

                        P2PMsg[] msgs = (P2PMsg[])msg["msgs"];
                        for (int i = 0; i < msgs.Length; i++)
                        {
                            Console.WriteLine(msgs[i].id);
                            Console.WriteLine(msgs[i].direction);
                            Console.WriteLine(msgs[i].mtype);
                            Console.WriteLine(msgs[i].mid);
                            Console.WriteLine(msgs[i].deleted);
                            Console.WriteLine(msgs[i].msg);
                            Console.WriteLine(msgs[i].attrs);
                            Console.WriteLine(msgs[i].mtime);
                        }
                   }

                    {
                   		Dictionary<string, dynamic> msg = await client.getGroupMessage(123, 100, true);
                        Console.WriteLine((int)msg["num"]);
                        Console.WriteLine((long)msg["lastid"]);
                        Console.WriteLine((long)msg["begin"]);
                        Console.WriteLine((long)msg["end"]);

                        GroupMsg[] msgs = (GroupMsg[])msg["msgs"];
                        for (int i = 0; i < msgs.Length; i++)
                        {
                            Console.WriteLine(msgs[i].id);
                            Console.WriteLine(msgs[i].from);
                            Console.WriteLine(msgs[i].mtype);
                            Console.WriteLine(msgs[i].mid);
                            Console.WriteLine(msgs[i].deleted);
                            Console.WriteLine(msgs[i].msg);
                            Console.WriteLine(msgs[i].attrs);
                            Console.WriteLine(msgs[i].mtime);
                        }
                    }

					{
                        Dictionary<string, dynamic> msg = await client.getRoomMessage(123, 100, true);
                        Console.WriteLine((int)msg["num"]);
                        Console.WriteLine((long)msg["lastid"]);
                        Console.WriteLine((long)msg["begin"]);
                        Console.WriteLine((long)msg["end"]);

                        RoomMsg[] msgs = (RoomMsg[])msg["msgs"];
                        for (int i = 0; i < msgs.Length; i++)
                        {
                            Console.WriteLine(msgs[i].id);
                            Console.WriteLine(msgs[i].from);
                            Console.WriteLine(msgs[i].mtype);
                            Console.WriteLine(msgs[i].mid);
                            Console.WriteLine(msgs[i].deleted);
                            Console.WriteLine(msgs[i].msg);
                            Console.WriteLine(msgs[i].attrs);
                            Console.WriteLine(msgs[i].mtime);
                        }
                    }

                   	{
                    	Dictionary<string, dynamic> msg = await client.getBroadcastMessage(100, true);
                        Console.WriteLine((int)msg["num"]);
                        Console.WriteLine((long)msg["lastid"]);
                        Console.WriteLine((long)msg["begin"]);
                        Console.WriteLine((long)msg["end"]);

                        BroadcastMsg[] msgs = (BroadcastMsg[])msg["msgs"];
                        for (int i = 0; i < msgs.Length; i++)
                        {
                            Console.WriteLine(msgs[i].id);
                            Console.WriteLine(msgs[i].from);
                            Console.WriteLine(msgs[i].mtype);
                            Console.WriteLine(msgs[i].mid);
                            Console.WriteLine(msgs[i].deleted);
                            Console.WriteLine(msgs[i].msg);
                            Console.WriteLine(msgs[i].attrs);
                            Console.WriteLine(msgs[i].mtime);
                        }
                    }


					} catch (Exception ex) {
						throw ex;
					}
                }
        }
 
}
