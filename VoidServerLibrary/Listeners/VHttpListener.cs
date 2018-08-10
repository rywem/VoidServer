using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using VoidServerLibrary.Interfaces;

namespace VoidServerLibrary.Listeners
{
    public class VHttpListener : IListener
    {

        public void Start(CancellationToken token, string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes missing");


            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            while (token.IsCancellationRequested == false)
            {
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string responseString;
                if (request.HasEntityBody)
                {

                    using (var reader = new StreamReader(request.InputStream,
                                                         request.ContentEncoding))
                    {
                        //responseString = reader.ReadToEnd()+ "<EOF>";

                        string requestString = reader.ReadToEnd();
                        Console.WriteLine(requestString);
                        Requests.CalculationRequest crequest = Newtonsoft.Json.JsonConvert.DeserializeObject<Requests.CalculationRequest>(requestString);
                        var calc = new Util.Calculator();
                        responseString = calc.Calculate(crequest).ToString();
                    }
                }
                else
                {
                    responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                }
                // Obtain a response object.
                // Construct a response.

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            listener.Stop();
        }
    }
}
