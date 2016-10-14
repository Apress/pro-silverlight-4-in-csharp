using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace MessengerServer
{
    public class PolicyConnection
    {
        private TcpClient client;
        private byte[] policy;

        public PolicyConnection(TcpClient client, byte[] policy)
        {
            this.client = client;
            this.policy = policy;
        }

        // The request that the client sends.
        private static string policyRequestString = "<policy-file-request/>";

        public void HandleRequest()
        {
            Stream s = client.GetStream();

            // Read the policy request string.
            byte[] buffer = new byte[policyRequestString.Length];
            // Only wait 5 seconds.
            client.ReceiveTimeout = 5000;
            s.Read(buffer, 0, buffer.Length);

            // Send the policy.
            s.Write(policy, 0, policy.Length);

            // Close the connection.
            client.Close();

            Console.WriteLine("Served policy file.");
        }
    }
}
