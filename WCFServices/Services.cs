using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    public class Services : WCFInterfaces.IServices
    {
        private void logCall(WCFInterfaces.STG message)
        {
            Console.WriteLine(message.statut_op);
            Console.WriteLine(message.info);
            Console.WriteLine(message.operationname);
            Console.WriteLine(message.tokenUser);
            Console.WriteLine(message.tokenApp);
            Console.WriteLine("Datas :");
            foreach (var i in message.data)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public string m_service(WCFInterfaces.STG message)
        {
            logCall(message);

            return "ok";
        }
    }
}
