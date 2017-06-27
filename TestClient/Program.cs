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
            string login = Console.ReadLine();

            Console.WriteLine("Endpoint = {0}", channelFactory.Endpoint.Address.ToString());

            string password = null;
            Console.WriteLine("('q' to quit)");

            do {
                Console.WriteLine("\nType login :");
                login = Console.ReadLine();
                Console.WriteLine("\nType password :");
                password = Console.ReadLine();

                if (login == "q") continue;
                if (password == "q") continue;

                STG message = new STG()
                {
                    statut_op = true,
                    info = "connection",
                    data = new object[2] { login, password },
                    operationname = "connection",
                    tokenApp = tokenApp,
                    tokenUser = "tokenUser"
                };

                try {
                    STG result = services.m_service(message);
                    Console.WriteLine("Result :");
                    result.Print();
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
