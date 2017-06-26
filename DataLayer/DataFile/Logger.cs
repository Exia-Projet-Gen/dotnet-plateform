using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataFile
{
    // Classe d'écriture de logs -- Singleton
    public class BddLogger
    {
        // Singleton
        private static BddLogger _instance = null;
        private static readonly object _padlock = new object();

        private string _logPath;

        // Efface les logs de la dernière execution
        private BddLogger()
        {
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bdd.log");

            using (StreamWriter writer = new StreamWriter(_logPath, false, Encoding.UTF8))
            {
                writer.WriteLine("Logs de l'execution du {0:dd/MM/yyyy} à {0:H:mm:ss}", DateTime.Now);
            }
        }

        // Récupération de l'instance unique
        public static BddLogger Instance()
        {
            if (_instance == null)
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new BddLogger();
                    }
                }
            }
            return _instance;
        }

        // Ajoute une entrée dans le fichier log
        public void WriteLog(string log)
        {
            using (StreamWriter writer = new StreamWriter(_logPath, true, Encoding.UTF8))
            {
                writer.WriteLine(log);
            }
        }
    }
}
