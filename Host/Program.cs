using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFServices.Services));
            host.AddServiceEndpoint(
                typeof(WCFInterfaces.IServices),
                new NetTcpBinding(),
                "net.tcp://localhost:2605/ServicesWCF"    
            );

            host.Open();

            Console.WriteLine("Services are listening...");
            Console.ReadLine();
            host.Close();
        }
    }
}
