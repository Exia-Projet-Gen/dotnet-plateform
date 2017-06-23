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
            channelFactory = new ChannelFactory<WCFInterfaces.IServices>(
                new NetTcpBinding(),
                "net.tcp://localhost:2605/ServicesWCF"
            );

            services = channelFactory.CreateChannel();

            Console.WriteLine("Ready ?");
            string login = Console.ReadLine();
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

                try {
                    result = services.Authenticate(login, password);
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
