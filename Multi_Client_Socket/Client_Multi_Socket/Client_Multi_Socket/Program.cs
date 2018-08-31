using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client_Multi_Socket
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1300;
            string IpAddress = "127.0.0.1";
            Socket Clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IpAddress), port);

            Clientsocket.Connect(ipep);

            Console.WriteLine("Client Connected ...");

            while (true)
            {
                string messageFromClient = null;
                Console.WriteLine("Enter the message ...");
                messageFromClient = Console.ReadLine();
                Clientsocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0, messageFromClient.Length,SocketFlags.None);

                byte[] msgFromServer = new byte[1024];
               int size =  Clientsocket.Receive(msgFromServer);
                Console.WriteLine("Server" + System.Text.Encoding.ASCII.GetString(msgFromServer,0, size));
            }
        }
    }
}
