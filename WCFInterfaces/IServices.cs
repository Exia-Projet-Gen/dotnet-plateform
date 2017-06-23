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
        string Authenticate(string login, string password);
    }
}
