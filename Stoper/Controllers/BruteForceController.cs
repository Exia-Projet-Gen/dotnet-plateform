using APIforJEE.Models;
using Stoper.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WCFInterfaces;

namespace APIforJEE.Controllers
{
    public class BruteForceController : ApiController
    {
        private ComWCF host = new ComWCF();

        [HttpPost]
        public object Post([FromBody] BruteForceModel.StopMessageModel stopMessage)
        {
            STG message = new STG()
            {
                statut_op = true,
                info = "",
                data = new object[] {
                    stopMessage.key,
                    stopMessage.fileName,
                    stopMessage.decodedText,
                    stopMessage.matchPercent,
                    stopMessage.mailAddress
                },
                operationname = "stopBruteForce",
                tokenApp = "",
                tokenUser = ""
            };

            host.send(message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
