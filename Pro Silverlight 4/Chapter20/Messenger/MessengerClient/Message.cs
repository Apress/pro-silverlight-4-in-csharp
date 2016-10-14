using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerClient
{
    public class Message
    {
        public string MessageText {get; set;}
        public string Sender {get; set;}
        public DateTime SendTime {get; set;}

        public Message(string messageText, string sender)
        {
            MessageText = messageText;
            Sender = sender;
            SendTime = DateTime.Now;
        }

        public Message() { }
    }
}
