using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WCFInterfaces;
using System.Web.Script.Serialization;

namespace BusinessLayer
{
    public class BruteForce
    {
        public STG BruteForceMessages(STG message)
        {

            message.Print();

            STG response = new STG()
            {
                statut_op = false,
                info = "",
                data = new object[0] { },
                operationname = message.operationname,
                tokenApp = "",
                tokenUser = message.tokenUser
            };

            foreach(string s in Encryption.GenerateKeys(1))
            {
                Encryption.Process(message.data[0].ToString(), s);
                Console.WriteLine(Encryption.Process(message.data[0].ToString(), s));

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.162.128.199:10080/dictionaryFacade-war/gen/dictionary/decode/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        decodedText = "Je suis content i am happy to live in france because of arabe all@gmail.com",
                        keyValue = "128fd",
                        fileName = "Text01"
                    }
                );

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }


            response.statut_op = true;
            response.info = "started";

            return response;

        }
    }
}
