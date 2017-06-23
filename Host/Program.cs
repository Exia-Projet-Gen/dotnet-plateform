using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFServices.Services));

            host.Open();

            foreach(ChannelDispatcher dispatcher in host.ChannelDispatchers)
            {
                Console.WriteLine("Binding = {0}", dispatcher.BindingName);

                foreach(EndpointDispatcher endPoint in dispatcher.Endpoints)
                {
                    Console.WriteLine("\tEndpoint = {0}", endPoint.EndpointAddress);
                }
            }

            Console.WriteLine("Services are listening...");
            Console.ReadLine();
            host.Close();
        }
    }
}
