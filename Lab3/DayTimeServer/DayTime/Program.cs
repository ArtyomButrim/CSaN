using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace DayTime
{
	class Program //132.163.96.4 - сервер для проверки
	{
		internal class Client
		{
			private static void Main(string[] args)
			{
				int type = 0;
				Console.WriteLine("Press 1 to select TCP or 2 to select UDP");
				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						type = 1;
						break;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						type = 2;
						break;
					default:
						Console.WriteLine("Wrong input.");
						Console.ReadKey();
						return;
				}

				if (type == 1)
				{
					var sock1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					sock1.Connect(GetMyIp(), 13);
					var buf1 = new byte[65535];
					var length1 = sock1.Receive(buf1);
					Console.WriteLine("\n" + Encoding.UTF8.GetString(buf1).Substring(0, length1));
					Console.ReadKey();
					return;
				}
				else
				{
					var sock2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					sock2.Bind(new IPEndPoint(GetMyIp(), 0));
					sock2.SendTo(Encoding.UTF8.GetBytes(""), new IPEndPoint(GetMyIp(), 13));
					var buf2 = new byte[65535];
					var length2 = sock2.Receive(buf2);
					Console.WriteLine("\n" + Encoding.UTF8.GetString(buf2).Substring(0, length2));
					Console.ReadKey();
					return;
				}
			}

			public static IPAddress GetMyIp()
			{
				return Dns.GetHostAddresses(Dns.GetHostName())
						.Where(address => address.AddressFamily == AddressFamily.InterNetwork)
						.ToArray()[0];
			}
		}
	}
}