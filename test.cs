using System;
using System.Text;
using System.Collections.Generic;

namespace com.rtm 
{
        class Test
        {
                static void Main(string[] args)
                {
                    RTMServerClient client = new RTMServerClient(11000001, "ef3617e5-e886-4a4e-9eef-7263c0320628", "52.83.245.22:13315");
					
					try {
						Console.WriteLine(client.sendMessage(123, 888, 2, "aaa", "bbb"));
						//Console.WriteLine(client.sendMessages(123, new long[] { 3, 4, 5, 888 }, 1, "aaa", "bbb"));
						//Console.WriteLine(client.sendGroupMessage(123, 222, 2, "xxx", "yyy"));
						//Console.WriteLine(client.sendRoomMessage(123, 999, 1, "xxx", "yyy"));
						//Console.WriteLine(client.broadcastMessage(123, 1, "xxx", "yyy"));
						//Console.WriteLine(client.addFriends(888, new long[] { 777,6,4 }));
						//Console.WriteLine(client.deleteFriends(888, new long[] { 4 }));
						//Console.WriteLine(client.getFriends(888));
						//Console.WriteLine(client.isFriend(888, 777));
						//Console.WriteLine(client.isFriends(888, new long[] {777,55,22}));
						//Console.WriteLine(client.addGroupMembers(123, new long[] {777,55,22}));
						//Console.WriteLine(client.deleteGroupMembers(123, new long[] {55,22}));
						//Console.WriteLine(client.deleteGroup(1233));
						//Console.WriteLine(client.getGroupMembers(123));
						//Console.WriteLine(client.isGroupMember(123, 777));
						//Console.WriteLine(client.getUserGroups(777));
						//Console.WriteLine(client.getToken(777));
						//Console.WriteLine(client.getOnlineUsers(new long[] {111, 222, 999}).Length);
						//Console.WriteLine(client.addGroupBan(123, 999, 30));
						//Console.WriteLine(client.removeGroupBan(123, 999));
						//Console.WriteLine(client.addRoomBan(333, 999, 30));
						//Console.WriteLine(client.removeRoomBan(333, 999));
						//Console.WriteLine(client.addProjectBlack(999, 60));
						//Console.WriteLine(client.removeProjectBlack(999));
						//Console.WriteLine(client.isBanOfGroup(123, 999));
						//Console.WriteLine(client.isProjectBlack(999));
						//Console.WriteLine(client.addRoomMember(123, 888));
						//Console.WriteLine(client.deleteRoomMember(123, 888));
						//Console.WriteLine(client.deleteMessage(123, 888, 333, 2));
						//Console.WriteLine(client.kickout(999));
						
                        
                        /*Dictionary<string, dynamic> msg = client.getP2PMessage(123, 888, 100, true);
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
                        }*/
                        
                        /*Dictionary<string, dynamic> msg = client.getBroadcastMessage(100, true);
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
                        }*/
                        
                        /*Dictionary<string, dynamic> msg = client.getRoomMessage(123, 100, true);
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
                        }*/

						/*Dictionary<string, dynamic> msg = client.getGroupMessage(123, 100, true);
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
                        }*/


					} catch (Exception ex) {
						throw ex;
					}
                }
        }
 
}
