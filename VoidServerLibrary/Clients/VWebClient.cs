using System;
using System.IO;
using System.Net;
using System.Text;
using VoidServerLibrary.Requests;

namespace VoidServerLibrary.Clients
{
    public class VWebClient : VoidServerLibrary.Interfaces.IClient
    {
        public string VResponse { get; set; }
        

        public void Send(CalculationRequest vrequest)
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(vrequest.URL);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  

            
            byte[] byte1 = ASCIIEncoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(vrequest));

            // Set the content type of the data being posted.
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(byte1, 0, byte1.Length);
            }
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            this.VResponse = reader.ReadToEnd();
            // Display the content.  
            
            //Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.  
            reader.Close();
            response.Close();
        }
    }
}
