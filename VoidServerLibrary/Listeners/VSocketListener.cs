using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using VoidServerLibrary.Interfaces;

namespace VoidServerLibrary.Listeners
{
    public class VSocketListener : IListener
    {
        bool start = true;
        string data = null;
        public void Start(string[] args)
        {
            //data buffer
            byte[] bytes = new byte[1024];
            //establish the local endpoint for the socket
            //dns.GetHostName returns the name of the host running the app
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");//ipHostInfo.AddressList[0];
            IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 8080);
            Console.WriteLine($"endpoint: {localEndpoint.ToString()}");
            //create the tcp/ip socket
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // bind the socket to the local endpoint and listen for incoming connections
            try
            {
                listener.Bind(localEndpoint);
                listener.Listen(10);
                //start listening for connections
                while (start == true)
                {
                    Console.WriteLine("Awaiting Connection... ");
                    Socket handler = listener.Accept();
                    data = null;
                    // an incoming connection needs to be processed
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }
                    //show the data on the console
                    Console.WriteLine("1");
                    byte[] msg = Encoding.ASCII.GetBytes(data);
                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("\nPress any key to continue... ");
            Console.Read();
        }
    }
}
