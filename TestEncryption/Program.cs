using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Windows.Forms;

namespace TestEncryption
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (string s in Encryption.GenerateKeys(2))
            {
                Encryption.Process("abc", s);
                Console.WriteLine(Encryption.Process("abc", s));
            }

            Console.ReadLine();
        }
    }
}
