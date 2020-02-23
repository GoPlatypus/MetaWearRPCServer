using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using TNT;

namespace MetaWearRPC
{
	class MetaWearRPC_Server
	{
		static void Main(string[] args)
		{
			//_MetaWearBoardsManagerTest(args);
			_ServerMain(args);
		}

		private static void _ServerMain(string[] args)
		{
			// Test.
			//ulong testMac = Global.MacFromString("F6:E9:DD:B4:CF:4A");

			try
			{
                string path;
                if (args.Length > 0)
                    path = args[0];
                else
                {
                    // C: \Users\Will\AppData\LocalLow\Platypus Neuro\PlatyDemo

                    path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low\\Platypus Neuro\\PlatyDemo";
                    path = Path.Combine(path, "mwboards.cfg");
                }

                Console.WriteLine("[MetaWearRPC_Server] Reading boards from " + path);

                // create a fake file if there isn't one there
                if(!File.Exists(path))
                {
                    File.WriteAllText(path, "EC:31:87:17:9E:BD\nD6:0E:AB:0D:C3:1E\nFF:DF:FA:75:18:D5\nD2:EA:9E:58:5F:2F\n");
                }

				using (MetaWearBoardsManager mwBoardsManager = new MetaWearBoardsManager(path))
				{
					MetaWearContract rpcContract = new MetaWearContract(mwBoardsManager);

					using (var rpcServer = TntBuilder
					.UseContract<IMetaWearContract>(rpcContract)
					.CreateTcpServer(IPAddress.Loopback, Global.ServerPort))
					{
						rpcServer.AfterConnect += RpcServer_AfterConnect;
						rpcServer.Disconnected += RpcServer_Disconnected;
						rpcServer.IsListening = true;
						Console.WriteLine("[MetaWearRPC_Server] Server listening to clients...");
                        Console.WriteLine(mwBoardsManager.DesiredBoards());
						Console.WriteLine("[MetaWearRPC_Server] Press Esc to exit...");

						//Test.
						//using (var rpcCient = TntBuilder
						//	.UseContract<IMetaWearContract>()
						//	.CreateTcpClientConnection(IPAddress.Loopback, Global.ServerPort))
						{

							while (true)
							{
								ConsoleKey key = Console.ReadKey().Key;

								if (key == ConsoleKey.Escape)
								{
									break;
								}
								//else if (key == ConsoleKey.NumPad1) // Test.
								//{
								//	rpcCient.Contract.StartMotorPattern(testMac, 5000, 100.0f, 100, 1);
								//	Console.WriteLine("[MetaWearRPC_Server] Client sent vibration.");
								//}
								//else if(key == ConsoleKey.NumPad2) // Stop Vibration Test.
								//{
								//	//rpcCient.Contract.StartMotor(testMac, 50, 100.0f);
								//	//int i = 0;
								//	//while(i++ < 500)
								//	{
								//		//rpcContract.StartMotorPattern(testMac, 50, 20.0f, 100, 1);
								//		rpcContract.StartMotorPattern(testMac, 5000, 1.0f, 100, 1);
								//	}
								//}
								Thread.Sleep(100);
							}
						}
					}
				}
				Console.WriteLine("[MetaWearRPC_Server] Server closing...");
				Thread.Sleep(2000);
			}
			catch (Exception e)
			{
				Console.WriteLine("[MetaWearRPC_Server] Error : " + e.Message);
				Console.ReadKey();
			}
		}

		private static void RpcServer_Disconnected(object arg1, TNT.Api.ClientDisconnectEventArgs<IMetaWearContract, TNT.Tcp.TcpChannel> arg2)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[MetaWearRPC_Server] Client Disconnected : " + arg2.Connection.Channel.LocalEndpointName);
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		private static void RpcServer_AfterConnect(object arg1, TNT.Api.IConnection<IMetaWearContract, TNT.Tcp.TcpChannel> arg2)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("[MetaWearRPC_Server] Client Connected : " + arg2.Channel.LocalEndpointName);
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		private static void _MetaWearBoardsManagerTest(string[] args)
		{
			//List<string> boardMacs = new List<string>()
			//{
			//	"F6:E9:DD:B4:CF:4A",
			//	"D2:80:93:BC:8C:FD",
			//	"DF:16:4D:D1:5D:58",
			//	"C2:48:ED:96:3B:74"
			//};

			try
			{

				//using (MetaWearBoardsManager mwBoardsManager = new MetaWearBoardsManager(boardMacs))
				using (MetaWearBoardsManager mwBoardsManager = new MetaWearBoardsManager(args[0]))
				{
					while (true)
					{
						ConsoleKey key = Console.ReadKey().Key;
						if (key == ConsoleKey.Escape)
						{
							break;
						}
						Thread.Sleep(100);
					}
				}
				Console.WriteLine("[MetaWearRPC_Server] MetaWearBoardsManager closing...");
				Thread.Sleep(2000);
			}
			catch (Exception e)
			{
				Console.WriteLine("[MetaWearRPC_Server] Error : " + e.Message);
				Console.ReadKey();
			}
		}
	}
}