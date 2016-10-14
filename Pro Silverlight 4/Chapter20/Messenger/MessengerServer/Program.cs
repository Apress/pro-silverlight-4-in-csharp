using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            PolicyServer policyServer = new PolicyServer("clientaccesspolicy.xml");
            policyServer.Start();
            Console.WriteLine("Policy server started.");            

            MessengerServer messengerServer = new MessengerServer();
            messengerServer.Start(); 
            Console.WriteLine("Messenger server started.");
            
            Console.WriteLine("Press Enter to exit.");
            // Wait for an enter key. You could also wait for a specific input
            // string (like "quit") or a single key using Console.ReadKey().
            Console.ReadLine();

            policyServer.Stop();
            Console.WriteLine("Policy server shut down.");

            messengerServer.Stop();
            Console.WriteLine("Messenger server shut down.");
        }
    }
}
