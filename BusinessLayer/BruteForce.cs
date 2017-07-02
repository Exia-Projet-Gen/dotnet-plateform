using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WCFInterfaces;
using System.Web.Script.Serialization;
using System.Threading;
using DataLayer.Mapping;
using DataLayer.Containers;

namespace BusinessLayer
{
    public class BruteForce
    {
        private static Dictionary<string, bool> stopBFs = new Dictionary<string, bool>();
        private static Dictionary<string, STG> infoBfs = new Dictionary<string, STG>();
        private static Files files = new Files();
        private static Mailer mailer = new Mailer();

        private JEE jee;
        private string file;
        private string text;
        private string user;

        public BruteForce(STG message, string username)
        {
            file = message.data[0].ToString();
            text = message.data[1].ToString();
            user = username;
            jee = new JEE();
            ServicePointManager.DefaultConnectionLimit = 1000;

            files.createFileInDatabase(file, user);

            if (stopBFs.ContainsKey(file))
            {
                stopBFs[file] = false;
            }
            else
            {
                stopBFs.Add(file, false);
            }
        }

        public static void StopBruteForce(STG message)
        {
            infoBfs[message.data[0].ToString()] = message;
            stopBFs[message.data[0].ToString()] = true;
        }

        public void BruteForceMessages()
        {
            Console.WriteLine("Start thread for file {0}", file);

            HttpWebResponse webResponse;

            foreach (string key in Encryption.GenerateKeys(6, "fjuaaa", "hjuioz"))
            {
                if (stopBFs[file])
                {
                    Console.WriteLine("JEE ask to stop thread for file {0}", file);
                    break;
                }

                Console.Write("call : {0} | ", key);

                /*
                webResponse = jee.sendDecryptedFile(
                    file,
                    key,
                    Encryption.Process(text, key)
                );
                
                int code = (int)webResponse.StatusCode;
                */
                int code = 202;

                Console.Write("{0} - {1}\n", key, code);

                if (code != 202)
                {
                    Console.WriteLine("Error while bruteforing {0} : JEE Error {1}", file, code);
                    break;
                }
            }

            Console.WriteLine("Stop thread for file {0}", file);

            files.StoreFileResult(file, user, infoBfs[file]);
            mailer.SendResultMail(file, user, infoBfs[file]);
        }
    }
}