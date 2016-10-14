using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace MessengerServer
{
    public class MessengerServer
    {
        private TcpListener listener;
        private int clientNum;
        private List<MessengerConnection> clients = new List<MessengerConnection>();

        public void Start()
        {
            // The allowed port range is 4502-4532.
            listener = new TcpListener(IPAddress.Any, 4530);
            listener.Start();
            
            // Wait for a connection request, 
            // and return a TcpClient initialized for communication. 
            // You could add code here to limit the maximum number of
            // concurrent connections.
            listener.BeginAcceptTcpClient(OnAcceptTcpClient, null);
        }

        private void OnAcceptTcpClient(IAsyncResult ar)
        {
            if (isStopped) return;

            // Listen for the next client.
            listener.BeginAcceptTcpClient(OnAcceptTcpClient, null);

            clientNum++;
            Console.WriteLine("Messenger client #" + clientNum.ToString() + " connected.");
            TcpClient client = listener.EndAcceptTcpClient(ar);

            // Create a new object to handle this connection.            
            MessengerConnection clientHandler = new MessengerConnection(client, "Client " +
                clientNum.ToString(), this);
            clientHandler.Start();                     
            
            lock (clients)
            {
                clients.Add(clientHandler);
            }
        }

        private bool isStopped;
        public void Stop()
        {
            isStopped = true;
            if (listener != null)
            {
                try
                {
                    listener.Server.Close();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }

            foreach (MessengerConnection client in clients)
            {
                client.Close();                
            }
        }
        
        public void DeliverMessage(byte[] message, int bytesRead)
        {
            Console.WriteLine("Delivering message.");

            // Duplicate the collection to prevent threading issues.
            MessengerConnection[] connectedClients;
            lock (clients)
            {
                connectedClients = clients.ToArray();
            }

            foreach (MessengerConnection client in connectedClients)
            {                
                try
                {
                    client.ReceiveMessage(message, bytesRead);                    
                }
                catch
                {
                    // Client is disconnected.
                    // Remove the client to avoid future attempts.
                    lock (clients)
                    {
                        clients.Remove(client);
                    }

                    client.Close();
                }
            }            
        }

    }
}
