﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    class Client
    {
        class Program
        {
            private static TcpClient _clientSocket = null;
            private static Stream _nstream = null;
            private static StreamWriter _sWriter = null;
            private static StreamReader _sReader = null;

            static void Main(string[] args)
            {
                try
                {
                   

                    // Step no : 1
                    //TCP establish connection via its socket through request to server

                    // Step no: 3 
                    // when TCP connection is established then client send (bytes of data) to server
                    //  we can use "localhost" , it mean our own local machine IP
                    // "127.0.0.1" loopback IP ( when Client and Server both used in a same machine)
                    //  Hostname ( server machine IP name --> 192.168.1.33)- Client and server are on the different machine 
                    // In client socket we should refer the server hostname in terms of IP address for this particular machine
                    using (_clientSocket = new TcpClient("127.0.0.1", 7777))
                    {
                        using (_nstream = _clientSocket.GetStream())
                        {
                            // Data will be flushed from the buffer to the stream after each write operation
                            _sWriter = new StreamWriter(_nstream) { AutoFlush = true };
                            Console.WriteLine("Client ready to...");
                            Console.WriteLine("Write  message here ");
                            string clientMsg = Console.ReadLine();
                            // client ready to send
                            // which has collected through user input
                            _sWriter.WriteLine(clientMsg);

                           
                            // Client recieved the (modified server Message) sent back by server to client 
                            // perform write operation 
                            _sReader = new StreamReader(_nstream);
                            string rdMsgFromServer = _sReader.ReadLine();
                            if (rdMsgFromServer != null)
                            {
                                Console.WriteLine(".....................................................");
                                Console.WriteLine("Client recieved Message from Server:" + rdMsgFromServer);
                                Console.WriteLine(".....................................................");

                            }
                            else
                            {
                                Console.WriteLine("Client recieved null message from Server ");
                            }
                        }
                    }

                    Console.WriteLine("Press enter to stop the client!");
                    Console.ReadKey();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }

        }
    }
}
