using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServer
{
    class Program
    {
        public static readonly int Port = 7777;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("raboti li");



            //public static readonly int Port = 7777;


            IPAddress localAddress = IPAddress.Loopback;
            TcpListener serverSocket = new TcpListener(localAddress, Port);
            serverSocket.Start();
            Console.WriteLine("The server strated on " + localAddress + "port" + Port);
            while (true)
            {
                try
                {
                    TcpClient clientConnection = serverSocket.AcceptTcpClient();
                    Task.Run(() => DoIt(clientConnection));
                }
                catch (SocketException)
                {
                    Console.WriteLine("Socket exeption : Will continue working");

                }
            }

        }

        private static void DoIt(TcpClient clientConnection)
        {
            Console.WriteLine("Incoming client " + clientConnection.Client);
            NetworkStream stream = clientConnection.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            while (true)
            {
                string request = reader.ReadLine();
                if (string.IsNullOrEmpty(request))
                {
                    break;

                }

                Console.WriteLine("Request:" + request);
                {
                    string response = request;
                    writer.WriteLine(response);
                    writer.Flush();
                }
                clientConnection.Close();
                Console.WriteLine("Socket closed");
            }

            Console.ReadKey();


        }
    }
}

