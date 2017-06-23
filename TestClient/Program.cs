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
            string text = Console.ReadLine();
            Console.WriteLine("('q' to quit)");
            string result = null;

            do {
                Console.WriteLine("\nType something :");
                text = Console.ReadLine();

                if (text == "q") continue;

                try {
                    result = services.Maj(text);
                    Console.WriteLine("Result :");
                    Console.WriteLine(result);
                }
                catch {
                    Console.WriteLine("Error !");
                    channelFactory.Abort();
                    throw;
                }

            } while(text != "q");
            
        }
    }
}
