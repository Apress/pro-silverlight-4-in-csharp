using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace MessengerServer
{
    public class MessengerConnection
    {
        private TcpClient client;
        private string ID;
        private MessengerServer server;

        public MessengerConnection(TcpClient client, string ID, MessengerServer server)
        {
            this.client = client;
            this.ID = ID;
            this.server = server;    
        }
                
        private byte[] message = new byte[1024];

        public void Start()
        {
            try
            {              
                // Listen for messages.
                client.Client.BeginReceive(message, 0, message.Length, 
                    SocketFlags.None, new AsyncCallback(OnDataReceived), null);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {                                
                int bytesRead = client.Client.EndReceive(asyn);

                if (bytesRead > 0)
                {
                    // Ask the server to send the message to all the clients.
                    server.DeliverMessage(message, bytesRead);
                    
                    // Listen for more messages.
                    client.Client.BeginReceive(message, 0, message.Length,
                      SocketFlags.None, new AsyncCallback(OnDataReceived), null);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message); 
            }
        }

        public void Close()
        {
            try
            {                
                client.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message); 
            }
        }

        public void ReceiveMessage(byte[] data, int bytesRead)
        {
            client.GetStream().Write(data, 0, bytesRead);
        }
    }
}
