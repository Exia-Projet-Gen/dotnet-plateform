using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCFInterfaces;

namespace TestClient
{
    class Program
    {
        static private ChannelFactory<IServices> channelFactory = null;
        static private IServices services = null;
        static private string tokenApp = "l{8W9Fs1p5hz;K6m.gx(vAr)BbkYHIgkH!$1rgtiUtA$BAcdXhUMOY:!5<0L62W"; 

        static void Main(string[] args)
        {
            channelFactory = new ChannelFactory<IServices>("tcpConfig");

            services = channelFactory.CreateChannel();

            Console.WriteLine("Ready ?");
            string action = Console.ReadLine();

            Console.WriteLine("Endpoint = {0}", channelFactory.Endpoint.Address.ToString());

            Console.WriteLine("('l' to login)");
            Console.WriteLine("('s' to signup)");
            Console.WriteLine("('q' to quit)");

            do {
                Console.WriteLine("\nChoose an action :");
                action = Console.ReadLine();

                if (action == "q") continue;

                STG message = new STG()
                {
                    statut_op = true,
                    info = "",
                    data = null,
                    operationname = "",
                    tokenApp = tokenApp,
                    tokenUser = ""
                };

                if (action == "l")
                {
                    Console.WriteLine("Login :");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password :");
                    string password = Console.ReadLine();

                    message.operationname = "login";
                    message.data = new object[] { username, password };
                }
                else if (action == "s")
                {
                    Console.WriteLine("Username :");
                    string username = Console.ReadLine();
                    Console.WriteLine("Email :");
                    string email = Console.ReadLine();
                    Console.WriteLine("Password :");
                    string password = Console.ReadLine();

                    message.operationname = "signup";
                    message.data = new object[] { username, email, password };
                }

                try {
                    STG result = services.m_service(message);
                    Console.WriteLine("\nResult :");
                    result.Print();
                }
                catch {
                    Console.WriteLine("\nError !");
                    channelFactory.Abort();
                    throw;
                }

            } while(action != "q");
            
        }
    }
}
