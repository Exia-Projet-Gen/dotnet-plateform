using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WCFInterfaces;
using System.Web.Script.Serialization;
using System.Threading;

namespace BusinessLayer
{
    public class BruteForce
    {
        private volatile bool shouldStop = false;
        private JEE jee;
        private string file;
        private string text;

        public BruteForce(STG message)
        {
            file = message.data[0].ToString();
            text = message.data[1].ToString();
            jee = new JEE();
            ServicePointManager.DefaultConnectionLimit = 1000;
        }

        public void BruteForceMessages()
        {
            Console.WriteLine(Encryption.Process(text, "fjuiop"));

            HttpWebResponse webResponse;

            foreach (string key in Encryption.GenerateKeys(6, "fjuioa", "fjuioz"))
            {
                if (shouldStop) break;

                Console.WriteLine("call : {0}", key);

                webResponse = jee.sendDecryptedFile(
                    file,
                    key,
                    Encryption.Process(text, key)
                );

                int code = (int)webResponse.StatusCode;

                Console.WriteLine("{0} - {1}", key, code);

                if (code != 202)
                {
                    Console.WriteLine("Error while bruteforing {0} : JEE Error {1}", file, code);
                    break;
                }
            }
        }
    }
}