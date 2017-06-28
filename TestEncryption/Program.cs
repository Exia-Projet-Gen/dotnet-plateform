using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.IO;

namespace TestEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading dictionnary...");
            string dicoPath = @"files/dico.txt";
            Dictionary<string, bool> dico = new Dictionary<string, bool>();
            string dicoArray = File.ReadAllText(dicoPath, Encoding.UTF8);
            string[] dicoSplit = dicoArray.Split('\n');
            bool test;

            foreach (string s in dicoSplit)
            {
                string t = s.Substring(0, s.Length - 1);

                try
                {
                    if (!dico.TryGetValue(t, out test))
                    {
                        dico.Add(t, true);
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("ERROR");
                    Console.WriteLine(t);
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Dico ready.");

            Console.WriteLine("Start key :");
            string start = Console.ReadLine();

            Console.WriteLine("Stop :");
            string stop = Console.ReadLine();

            string path = @"files/P1A.txt";
            string resultPath = @"files/results/";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                Console.WriteLine("Can't find the file");
                return;
            }

            string text = File.ReadAllText(path, Encoding.UTF8);

            foreach (string key in Encryption.GenerateKeys(6, start, stop))
            {
                string encrypt = Encryption.Process(text, key);
                string[] words = encrypt.Split(' ');

                long max = words.Length;
                long count = 0;

                foreach (string s in words)
                {
                    if (dico.TryGetValue(s, out test))
                    {
                        count++;
                    }
                }

                long percent = (count / max) * 100;

                Console.WriteLine("{0} - {1}", key, percent);

                if (percent > 50)
                {
                    Console.WriteLine("Saved !");
                    File.WriteAllText(resultPath + key + ".txt", encrypt, Encoding.UTF8);
                }
            }

            Console.WriteLine("Finish !");
            Console.ReadLine();
        }
    }
}