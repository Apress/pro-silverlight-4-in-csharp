using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MessengerClient
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        // The socket for the underlying connection.
        private Socket socket;

        private void cmdConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((socket != null) && (socket.Connected == true)) socket.Close();
            }
            catch (Exception err)
            {
                AddMessage("ERROR: " + err.Message);
            }

            DnsEndPoint endPoint =
              new DnsEndPoint(Application.Current.Host.Source.DnsSafeHost, 4530);
            socket = new Socket(AddressFamily.InterNetwork,
              SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.UserToken = socket;
            args.RemoteEndPoint = endPoint;
            args.Completed +=
              new EventHandler<SocketAsyncEventArgs>(OnSocketConnectCompleted);
            socket.ConnectAsync(args);
        }

        private void OnSocketConnectCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (!socket.Connected)
            {
                AddMessage("Connection failed.");
                return;
            }

            AddMessage("Connected to server.");

            // Messages can be a maximum of 1024 bytes.
            byte[] response = new byte[1024];
            e.SetBuffer(response, 0, response.Length);
            e.Completed -=
              new EventHandler<SocketAsyncEventArgs>(OnSocketConnectCompleted);
            e.Completed += new EventHandler<SocketAsyncEventArgs>(OnSocketReceive);            
            
            // Listen for messages.
            socket.ReceiveAsync(e);
        }

        private void OnSocketReceive(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0)
            {
                AddMessage("Server disconnected.");
                try
                {
                    socket.Close();
                }
                catch { }
                return;              
            }
            
            try
            {
                // Retrieve and display the message.                
                XmlSerializer serializer = new XmlSerializer(typeof(Message));
                MemoryStream ms = new MemoryStream();
                ms.Write(e.Buffer, 0, e.BytesTransferred);
                ms.Position = 0;
                Message message = (Message)serializer.Deserialize(ms);

                AddMessage("[" + message.Sender + "] " + message.MessageText +
                    " (at " + message.SendTime.ToLongTimeString() + ")");
            }
            catch (Exception err)
            {
                AddMessage("ERROR: " + err.Message);
            }
            
            // Listen for more messages.
            socket.ReceiveAsync(e);
        }              

        private void cmdSend_Click(object sender, RoutedEventArgs e)
        {
            if ((socket == null) || (socket.Connected == false))
            {
                AddMessage("ERROR: Not connected.");
                return;
            }

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();

            // Prepare the message.
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, new Message(txtMessage.Text, txtName.Text));
            byte[] messageData = ms.ToArray();
            // (You could check the 1024 message limit here.)
            List<ArraySegment<byte>> bufferList = new List<ArraySegment<byte>>();
            bufferList.Add(new ArraySegment<byte>(messageData));
            args.BufferList = bufferList;

            // Send the message.
            socket.SendAsync(args);
        }

        // Add to the label, making sure the code runs on the user interface thread.
        private void AddMessage(string message)
        {
            Dispatcher.BeginInvoke(
                    delegate()
                    {
                        lblMessages.Text += message + "\n";
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight);
                    });
        }
    }
}
