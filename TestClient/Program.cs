using System;
using System.ServiceModel;
using WCFInterfaces;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text;

namespace TestClient
{
    class Program
    {
        static private ChannelFactory<IServices> channelFactory = null;
        static private IServices services = null;
        static private string tokenApp = "l{8W9Fs1p5hz;K6m.gx(vAr)BbkYHIgkH!$1rgtiUtA$BAcdXhUMOY:!5<0L62W";

        [STAThread]
        static void Main(string[] args)
        {
            channelFactory = new ChannelFactory<IServices>("tcpConfig");

            services = channelFactory.CreateChannel();

            Console.WriteLine("Ready ?");
            string action = Console.ReadLine();
            string tokenUser = "";

            Console.WriteLine("Endpoint = {0}", channelFactory.Endpoint.Address.ToString());

            Console.WriteLine("('l' to login)");
            Console.WriteLine("('o' to logout)");
            Console.WriteLine("('s' to signup)");
            Console.WriteLine("('c' to check token user)");
            Console.WriteLine("('bf' to send file for bruteforce)");
            Console.WriteLine("('r' to ask for file results)");
            Console.WriteLine("('g' to get the list of your files)");
            Console.WriteLine("('q' to quit)");

            do {
                Console.WriteLine("\nChoose an action :");
                action = Console.ReadLine();

                if (action == "q") continue;

                STG message = new STG()
                {
                    statut_op = true,
                    info = "",
                    data = new object[] { },
                    operationname = "",
                    tokenApp = tokenApp,
                    tokenUser = tokenUser
                };

                if (action == "l")
                {
                    Console.WriteLine("Login :");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password :");
                    string password = Console.ReadLine();

                    message.operationname = "login";
                    message.data = new object[] { username, password };
                }
                else if (action == "s")
                {
                    Console.WriteLine("Username :");
                    string username = Console.ReadLine();
                    Console.WriteLine("Email :");
                    string email = Console.ReadLine();
                    Console.WriteLine("Password :");
                    string password = Console.ReadLine();

                    message.operationname = "signup";
                    message.data = new object[] { username, email, password };
                }
                else if (action == "c")
                {
                    message.operationname = "checkTokenUser";
                }
                else if (action == "o")
                {
                    message.operationname = "logout";
                }
                else if (action =="bf")
                {
                    String FileContent = null;
                    Console.WriteLine("Insérer un document : ");
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.DefaultExt = ".txt";
                    //ofd.Filter = "Document texte (.txt)/*.txt";
                    ofd.ShowDialog();
                    string filename = ofd.FileName;
                    Console.WriteLine("Document : " + filename);
                    FileContent = File.ReadAllText(filename, Encoding.UTF8);
                    Console.WriteLine("Content : " + FileContent);

                    message.operationname = "bruteForce";
                    message.data = new object[] { Path.GetFileName(filename), FileContent };
                }
                else if (action == "r")
                {
                    Console.WriteLine("File :");
                    string file = Console.ReadLine();

                    message.operationname = "result";
                    message.data = new object[] { file };
                }
                else if (action == "g")
                {
                    message.operationname = "list";
                    message.data = new object[] { };
                }

                try {
                    STG result = services.m_service(message);

                    if(result.operationname == "login")
                    {
                        tokenUser = result.tokenUser;
                    }

                    Console.WriteLine("\nResult :");
                    result.Print();
                }
                catch {
                    Console.WriteLine("\nError !");
                    channelFactory.Abort();
                    throw;
                }

            } while(action != "q");
            
        }
    }
}
