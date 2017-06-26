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
        private void logCall(params object[] parameters)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            StringBuilder stringBuilder = new StringBuilder();
            ParameterInfo[] parametersInfo = methodBase.GetParameters();

            for (int index= 0; index < parametersInfo.Length; index++)
            {
                stringBuilder.AppendFormat(" [{0}={1}]", parametersInfo[index].Name, parameters[index]);
            }

            Console.WriteLine("{0} : {1}", methodBase.Name, stringBuilder);
        }
        public string Authenticate(string login, string password)
        {
            logCall(new object[2] { login, password });
            return "ok";
        }

        public string SendFiles(string FilesContent)
        {
            Console.WriteLine("Files sended");
            logCall(FilesContent);
            return "ok";
        }
    }
}
