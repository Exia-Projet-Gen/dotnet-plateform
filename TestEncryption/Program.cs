using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace TestEncryption
{
    class Program
    {
        static void Main(string[] args)
        {


            foreach(string s in Encryption.GenerateKeys(4))
            {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }
    }
}
