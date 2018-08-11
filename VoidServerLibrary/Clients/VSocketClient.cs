using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using VoidServerLibrary.Requests;

namespace VoidServerLibrary.Clients
{
    public class VSocketClient : Interfaces.IClient
    {
        public string VResponse { get; set; }

        public void Send(CalculationRequest vrequest)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPAddress.Parse(vrequest.URL);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);
                    Console.WriteLine($"Socket connected to {sender.RemoteEndPoint}");
                    string requestString = Newtonsoft.Json.JsonConvert.SerializeObject(vrequest);
                    byte[] msg = Encoding.ASCII.GetBytes($"{requestString}<EOF>");
                    //send the data through the socket
                    int bytesSent = sender.Send(msg);
                    //receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    VResponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException argsNullEx)
                {
                    Console.WriteLine($"Argument null exception {argsNullEx}");
                }
                catch (SocketException socketException)
                {
                    Console.WriteLine($"SocketException : {socketException}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected exception caught {ex}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected (outer) exception caught {e}");
            }
        }
    }
}
