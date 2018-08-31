using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoClientExample
{
    class Client2
    {
        public static void Main()
        {
            try
            {
                // create a client and bind it with IPAddress And Port number
                TcpClient client = new TcpClient("127.0.0.1", 8080);

                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                // empty string
                string s = string.Empty;

                while (!s.Equals("Exit"))
                {
                    Console.WriteLine("Enter a String to Send The server");
                    s = Console.ReadLine();
                    Console.WriteLine();
                    writer.WriteLine(s);
                    writer.Flush();

                    string serverString = reader.ReadLine();
                    Console.WriteLine(serverString);
                }
                reader.Close();
                writer.Close();
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
