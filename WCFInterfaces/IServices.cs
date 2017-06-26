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
        string m_service(STG message);
    }

    public struct STG
    {
        public bool statut_op { get; set; }
        public string info { get; set; }
        public object[] data { get; set; }
        public string operationname { get; set; }
        public string tokenApp { get; set; }
        public string tokenUser { get; set; }
    }
}
