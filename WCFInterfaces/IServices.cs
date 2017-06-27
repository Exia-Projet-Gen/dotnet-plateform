using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel; 

namespace WCFInterfaces
{
    [ServiceContract]
    public interface IServices
    {
        [OperationContract]
        STG m_service(STG message);
    }

    public struct STG
    {
        public bool statut_op { get; set; }
        public string info { get; set; }
        public object[] data { get; set; }
        public string operationname { get; set; }
        public string tokenApp { get; set; }
        public string tokenUser { get; set; }

        public void Print()
        {
            Console.WriteLine(statut_op);
            Console.WriteLine(info);
            Console.WriteLine(operationname);
            Console.WriteLine(tokenUser);
            Console.WriteLine(tokenApp);
            Console.WriteLine("Datas :");
            foreach (var i in data)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}
