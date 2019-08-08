using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoidServerLibrary.Interfaces;

namespace VoidServerLibrary.Listeners
{
    public class VHttpListener : IListener
    {
        HttpListener listener;
        public void Start(string[] prefixes, CancellationToken token)
        {
            try
            {
                if (token != null)
                    token.ThrowIfCancellationRequested();
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
                listener = new HttpListener();

                // Add the prefixes.
                foreach (string s in prefixes)
                {
                    listener.Prefixes.Add(s);
                }

                listener.Start();
                Console.WriteLine("Listening... on  " + prefixes[0]);
                while (token.IsCancellationRequested == false || listener.IsListening == true)
                {
                    try
                    {
                        IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
                        // Applications can do some work here while waiting for the 
                        // request. If no work can be done until you have processed a request,
                        // use a wait handle to prevent this thread from terminating
                        // while the asynchronous operation completes.                

                        result.AsyncWaitHandle.WaitOne();
                        Console.WriteLine("Request processed asyncronously.");
                        if (result == null)
                            break;
                    }
                    catch (Exception ex)
                    {
                        return;
                        //break;
                    }

                }
                if (listener != null && listener.IsListening == true)
                {
                    listener.Close();
                    listener.Stop();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void Stop()
        {
            if (listener != null && listener.IsListening == true)
                listener.Close();
            if (listener != null && listener.IsListening == true)
                listener.Stop();
        }


        public void ListenerCallback(IAsyncResult result)
        {
            try
            {
                HttpListener listener = (HttpListener)result.AsyncState;
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string responseString;
            if (request.HasEntityBody)
            {

                using (var reader = new StreamReader(request.InputStream,
                                                     request.ContentEncoding))
                {
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
                //listener.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }



        private void Process(object o)
        {
            var context = o as HttpListenerContext;
            //context = listener.GetContext();
            Process(context);
        }

        private void Process(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            string responseString;
            if (request.HasEntityBody)
            {

                using (var reader = new StreamReader(request.InputStream,
                                                     request.ContentEncoding))
                {
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
        
    }
}
