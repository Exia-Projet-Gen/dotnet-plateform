using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using BusinessLayer;

namespace Crypter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string fileContent = null;
            Console.WriteLine("Select a file : ");

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = ".txt";
            ofd.ShowDialog();

            string filePath = ofd.FileName;
            string fileDir = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileName(filePath);

            Console.WriteLine("Chemin : " + filePath);
            Console.WriteLine("Repertoire : " + fileDir);
            Console.WriteLine("Fichier : " + fileName);

            Console.WriteLine("\nEntrer une clé :");
            string key = Console.ReadLine();

            fileContent = File.ReadAllText(filePath, Encoding.UTF8);
            string decodedPath = fileDir + "/decoded" + fileName;

            File.WriteAllText(decodedPath, Encryption.Process(fileContent, key), Encoding.UTF8);
        }
    }
}
