using BusinessLayer;
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
        private string tokenApp = "l{8W9Fs1p5hz;K6m.gx(vAr)BbkYHIgkH!$1rgtiUtA$BAcdXhUMOY:!5<0L62W";
        private Authentication auth = new Authentication();

        public STG m_service(STG message)
        {
            message.Print();

            STG response = new STG()
            {
                statut_op = false,
                info = message.info,
                data = new object[0] { },
                operationname = message.operationname,
                tokenApp = message.tokenApp,
                tokenUser = message.tokenUser
            };

            if (message.tokenApp != tokenApp)
            {
                response.statut_op = false;
                response.info = "Wrong tokenApp";

                return response;
            }

            Authentication auth = new Authentication();

            if(message.operationname == "login")
            {
                response = auth.Login(message);
            }
            else if(message.operationname == "signup")
            {
                response = auth.Signup(message);
            }

            return response;
        }
    }
}
