using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFInterfaces;

namespace WCFServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
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
                tokenApp = "",
                tokenUser = message.tokenUser
            };

            if (message.tokenApp != tokenApp)
            {
                response.statut_op = false;
                response.info = "Wrong tokenApp";

                return response;
            }

            if(message.operationname == "stopBruteForce")
            {
                BruteForce.StopBruteForce(message.data[1].ToString());
                response.statut_op = true;
                return response;
            }

            Authentication auth = new Authentication();

            if(message.operationname == "login")
            {
                response = auth.Login(message);
                return response;
            }
            else if(message.operationname == "signup")
            {
                response = auth.Signup(message);
                return response;
            }
            else if (message.operationname == "checkTokenUser")
            {
                response.statut_op = auth.VerifyTokenUser(message.tokenUser);
                return response;
            }
            else if(message.operationname == "logout")
            {
                response = auth.Logout(message);
                return response;
            }

            if(!auth.VerifyTokenUser(message.tokenUser))
            {
                response.info = "Unknown token user";
                return response;
            }

            if (message.operationname == "bruteForce")
            {
                BruteForce workerObject = new BruteForce(message);
                Thread workerThread = new Thread(workerObject.BruteForceMessages);

                workerThread.Start();
                while (!workerThread.IsAlive);

                response.statut_op = true;
                response.info = "started";
                return response;
            }

            return response;
        }
    }
}
