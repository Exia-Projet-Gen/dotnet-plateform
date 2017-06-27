using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WCFInterfaces;

namespace WCFServices
{
    public class Services : IServices
    {
        static private string tokenApp = "l{8W9Fs1p5hz;K6m.gx(vAr)BbkYHIgkH!$1rgtiUtA$BAcdXhUMOY:!5<0L62W";

        public STG m_service(STG message)
        {
            message.Print();

            if(message.tokenApp != tokenApp)
            {
                return new STG()
                {
                    statut_op = false,
                    info = message.info,
                    data = null,
                    operationname = message.operationname,
                    tokenApp = message.tokenApp,
                    tokenUser = message.tokenUser
                };
            }

            STG response = new STG()
            {
                statut_op = true,
                info = "response",
                data = new object[1] { "test" },
                operationname = "response",
                tokenApp = tokenApp,
                tokenUser = "tokenUser"
            };

            return response;
        }
    }
}
