using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BusinessLayer
{
    public class JEE
    {
        private string sendDecodedUri = "http://10.162.128.199:10080/exia-rest-crud/crud/decodedFile";

        public HttpWebResponse sendDecryptedFile(string fileName, string key, string decodedFile)
        {
            return send(sendDecodedUri, "POST", new JavaScriptSerializer().Serialize(new
            {
                decodedText = decodedFile,
                keyValue = key,
                fileName = fileName
            }));
        }

        private HttpWebResponse send(string uri, string command, string serializedJson)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = command;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(serializedJson);
            }

            try
            {
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException ex)
            {
                return (HttpWebResponse)ex.Response;
            }
        }
    }
}
