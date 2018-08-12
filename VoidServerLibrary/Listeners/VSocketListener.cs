using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using VoidServerLibrary.Interfaces;

namespace VoidServerLibrary.Listeners
{
    public class VSocketListener : IListener
    {
        //bool start = true;
        
        object _listener { get; set; }

        string data = null;
        public void Start(string[] args, CancellationToken token)
        {
            try
            {
                //data buffer
                byte[] bytes = new byte[1024];
                //establish the local endpoint for the socket
                //dns.GetHostName returns the name of the host running the app
                IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPAddress.Parse(args[0]);
                IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 8080);
                Console.WriteLine($"endpoint: {localEndpoint.ToString()}");
                //create the tcp/ip socket
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _listener = listener as object;
                // bind the socket to the local endpoint and listen for incoming connections
                try
                {
                    listener.Bind(localEndpoint);
                    listener.Listen(10);
                    //start listening for connections
                    while (token.IsCancellationRequested==false)
                    {
                        try
                        {


                            Console.WriteLine("Awaiting Connection... ");
                            Socket handler = listener.Accept();
                            data = null;
                            // an incoming connection needs to be processed
                            int count = 0;
                            while (true)
                            {
                                int bytesRec = handler.Receive(bytes);
                                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                if (data.IndexOf("<EOF>") > -1 || count > 5000)
                                    break;
                                count++;
                            }
                            data = data.Substring(0, data.Length - 5);
                            Requests.CalculationRequest crequest = Newtonsoft.Json.JsonConvert.DeserializeObject<Requests.CalculationRequest>(data);
                            var calc = new Util.Calculator();
                            string responseString = calc.Calculate(crequest).ToString();
                            //show the data on the console
                            byte[] msg = Encoding.ASCII.GetBytes(responseString);
                            handler.Send(msg);
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                        }
                        catch(Exception ex)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Console.WriteLine("\nPress any key to continue... ");
                Console.Read();
            }
            catch(Exception ex)
            {
                return;
            }
        }

        public void Stop()
        {
            return;
            //throw new NotImplementedException();
            //Socket listener = _listener as Socket;
            //listener.Shutdown(SocketShutdown.Both);
        }
    }
}
