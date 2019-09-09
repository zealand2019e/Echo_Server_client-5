using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServer
{
    class Server
    {
        public static readonly int Port = 7777; //we are just choosing it cuz it it some by defalt or smt

        static void Main(string[] args)
           {
            Console.WriteLine("Hello World!");
            Console.WriteLine("raboti li");



            //public static readonly int Port = 7777;


            IPAddress localAddress = IPAddress.Loopback; 
            TcpListener serverSocket = new TcpListener(localAddress, Port); // we are saying we are opening the TCPListener stating the IP and the Port as Parameters 
            serverSocket.Start();// we are starting the Listener, turning it on hahah
            Console.WriteLine("The server strated on " + localAddress + "port" + Port); // Just to display if it worked 
            while (true)
            {
                try
                {
                    TcpClient clientConnection = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Client Connected");
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
            writer.AutoFlush = true;
            // Auto flush 
            while (true)
            {
                string request = reader.ReadLine();
                Console.WriteLine("Recive request" + request);
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


            }
            clientConnection.Close();
            Console.WriteLine("Socket closed");
            Console.ReadKey();


        } 

    }
}
