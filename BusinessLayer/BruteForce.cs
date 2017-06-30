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
        private static Dictionary<string, bool> stopBFs = new Dictionary<string, bool>();

        private JEE jee;
        private string file;
        private string text;

        public BruteForce(STG message)
        {
            file = message.data[0].ToString();
            text = message.data[1].ToString();
            jee = new JEE();
            ServicePointManager.DefaultConnectionLimit = 1000;

            if(stopBFs.ContainsKey(file))
            {
                stopBFs[file] = false;
            }
            else
            {
                stopBFs.Add(file, false);
            }
        }

        public static void StopBruteForce(string file)
        {
            stopBFs[file] = true;
        }

        public void BruteForceMessages()
        {
            Console.WriteLine("Start thread for file {0}", file);

            HttpWebResponse webResponse;

            foreach (string key in Encryption.GenerateKeys(6, "fjuioa", "hjuioz"))
            {
                if (stopBFs[file])
                {
                    Console.WriteLine("JEE ask to stop thread for file {0}", file);
                    break;
                }

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

            Console.WriteLine("Stop thread for file {0}", file);
        }
    }
}