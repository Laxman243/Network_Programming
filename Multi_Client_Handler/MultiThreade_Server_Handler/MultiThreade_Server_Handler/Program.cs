using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace MultiThreade_Server_Handler
{
    class MultiThreadedEchoServer
    {
        // Method for ProcessClientRequest
        private static void ProcessClientRequest(object argument)
        {
            // casting an argument into TcpClient
            TcpClient client = (TcpClient)argument;

            // take inputs 
            try
            {
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                // create a Empty String
                string s = string.Empty;

                // a loop which display the data on server and client side 
                while (!(s = reader.ReadLine()).Equals("Exit") || (s == null))
                {
                    // print on server
                    Console.WriteLine("From Client -> " + s);
                    // print on client
                    writer.WriteLine("From server -> " + s);
                    writer.Flush();
                }
                // close reader / writer and client
                reader.Close();
                writer.Close();
                client.Close();

                Console.WriteLine("Closing the Client Connection");
            }

            catch (IOException)
            {
                Console.WriteLine("Problem with clent Communication Exiting Thread.... ");
            }
            // closing the resoures 
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }

        // MAIN METHODS 
        public static void Main()
        {
            // First and fore Mostthing is create TcpListener
            TcpListener listener = null; // initially listener is null

            try
            {
                // binding the listener with Unique IPAdrress and Port number
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
                listener.Start();
                Console.WriteLine("MultiThreadedEchoServer started ..... ");

                while (true)
                {
                    Console.WriteLine("Waiting For incoming client Connection .... ");
                    // accept the client --> now start talking
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Accepted new Client Connection ... ");

                    // create Thread --> so whenever ProcessClientRequest passes it create a new Thread and starts 
                    Thread t = new Thread(ProcessClientRequest);
                    t.Start(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // close the listener to stop listening if any new connection is coming
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }

    }
    
}
