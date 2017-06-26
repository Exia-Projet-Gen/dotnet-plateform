using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TestClient
{
    class Program
    {
        static private ChannelFactory<WCFInterfaces.IServices> channelFactory = null;
        static private WCFInterfaces.IServices services = null;

        static void Main(string[] args)
        {
            channelFactory = new ChannelFactory<WCFInterfaces.IServices>("tcpConfig");

            services = channelFactory.CreateChannel();

            Console.WriteLine("Ready ?");
            string login = Console.ReadLine();

            Console.WriteLine("Endpoint = {0}", channelFactory.Endpoint.Address.ToString());

            string password = null;
            Console.WriteLine("('q' to quit)");
            string result = null;

            do {
                Console.WriteLine("\nType login :");
                login = Console.ReadLine();
                Console.WriteLine("\nType password :");
                password = Console.ReadLine();

                if (login == "q") continue;
                if (password == "q") continue;

                WCFInterfaces.STG message = new WCFInterfaces.STG() {
                    statut_op = true,
                    info = "connection",
                    data = new object[2] { login, password },
                    operationname = "connection",
                    tokenApp = "tokenApp",
                    tokenUser = "tokenUser"
                };

                try {
                    result = services.m_service(message);
                    Console.WriteLine("Result :");
                    Console.WriteLine(result);
                }
                catch {
                    Console.WriteLine("Error !");
                    channelFactory.Abort();
                    throw;
                }

            } while(login != "q" && password != "q");
            
        }
    }
}
