using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace MessengerServer
{ 
    public class PolicyServer
    {        
        private byte[] policy;
        private TcpListener listener;        
    
        public PolicyServer(string policyFile)
        {
            // Load the policy file.
            FileStream policyStream = new FileStream(policyFile, FileMode.Open);
            policy = new byte[policyStream.Length];
            policyStream.Read(policy, 0, policy.Length);
            policyStream.Close();
        }

        public void Start()
        {
            // Create the listener.
            listener = new TcpListener(IPAddress.Any, 943);
            listener.Start();
            
            // Wait for a connection.
            listener.BeginAcceptTcpClient(OnAcceptTcpClient, null);
        }
                
        public void OnAcceptTcpClient(IAsyncResult ar)
        {
            if (isStopped) return;

            Console.WriteLine("Received policy request.");

            // Wait for the next connection.
            listener.BeginAcceptTcpClient(OnAcceptTcpClient, null);

            // Handle this connection.
            try
            {
                TcpClient client = listener.EndAcceptTcpClient(ar);

                PolicyConnection policyConnection = new PolicyConnection(client, policy);
                policyConnection.HandleRequest();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private bool isStopped;
        public void Stop()
        {
            isStopped = true;

            try
            {
                listener.Stop();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }    
}
