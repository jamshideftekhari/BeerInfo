using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BeerInfo;
using Newtonsoft.Json;

namespace BeerServer
{
    public static class BeerListener
    {
        private static int clientNr = 0;
        public static TcpListener StartListener()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Loopback, 4646);
            serverSocket.Start();
            Console.WriteLine("server started waiting for client connection");
            return serverSocket;
        }

        public static TcpClient GetTcpClient(TcpListener socket)
        {
            TcpClient clientConnection = socket.AcceptTcpClient();
            clientNr++;
            Console.WriteLine("Client " + clientNr + "connected");
            return clientConnection;
        }

        public static void ReadWriteStream(TcpClient client)
        {
            try
            {
                Stream ns = client.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                string message = sr.ReadLine();
                string answer = "";

                while (message != " " && message != "stop")
                {
                    Console.WriteLine("Client: " + message);
                    switch (message)
                    {
                        case "GetAll":
                            //answer = Beers.GetAll().ToString();
                            Console.WriteLine(JsonConvert.SerializeObject(Beers.BeerList));
                            sw.WriteLine(JsonConvert.SerializeObject(Beers.BeerList));
                            break;
                        case "GetById":
                            int id = Int32.Parse(sr.ReadLine());
                            answer = Beers.GetById(id).ToString();
                            Console.WriteLine(answer);
                            sw.WriteLine(answer);
                            break;
                        case "Save":
                            string jsonString = sr.ReadLine();
                            Beer beer = JsonConvert.DeserializeObject<Beer>(jsonString);
                            Beers.AddBeer(beer);
                            break;
                        default:
                            answer = "Bad Request";
                            Console.WriteLine(answer);
                            sw.WriteLine(answer);
                            break;
                    }
                    message = sr.ReadLine();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
