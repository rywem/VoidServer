﻿using System;
using System.IO;
using System.Net;

namespace VoidServerLibrary.Clients
{
    public class VWebClient : VoidServerLibrary.Interfaces.IClient
    {

        string[] args;
        public VWebClient(string[] args)
        {

        }

        public void Send()
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(args[0]);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.  
            reader.Close();
            response.Close();
        }
    }
}
