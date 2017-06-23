using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel; 

namespace WCFInterfaces
{
    [ServiceContract]
    public interface IMesServices
    {
        [OperationContract]
        string Authenticate(string login, string password);
    }
}
