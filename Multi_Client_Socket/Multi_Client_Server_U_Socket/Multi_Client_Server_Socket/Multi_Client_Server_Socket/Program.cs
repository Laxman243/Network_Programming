using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Multi_Client_Server_Socket
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1300;
            string IpAddress = "127.0.0.1";

            Socket serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IpAddress), port);

            serverListener.Bind(ipep);
            serverListener.Listen(10);

            Console.WriteLine("Server starting ....");

            Socket clientSocket = default(Socket);

            Program p = new Program(); 
            int counter = 0;

            while (true)
            {
                counter++;
                clientSocket = serverListener.Accept();
                Console.WriteLine(counter + "client connected ... ");
                Thread t = new Thread(new ThreadStart(() => p.User(clientSocket)));
                t.Start();
            }
        }
        public void User(Socket client)
        {
            while(true)
            {
                byte[] msg= new byte[1024];
                int size = client.Receive(msg);
                client.Send(msg, 0 ,size ,SocketFlags.None);
            }
        }
    }
}
