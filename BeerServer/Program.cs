using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BeerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("server starting listener!");

            // start listener , get tcp client, read write
            //BeerListener.ReadWriteStream(BeerListener.GetTcpClient(BeerListener.StartListener()));

            // concurrent server
            TcpListener socket = BeerListener.StartListener();
            while (true)
            {
                Task.Run((() => BeerListener.ReadWriteStream(BeerListener.GetTcpClient(socket))));

            }
            
        }
    }
}
