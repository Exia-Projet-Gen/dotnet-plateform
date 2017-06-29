using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Encryption
    {
        public static string Process(string text, string keystring)
        {

            char[] key = keystring.ToCharArray();
            char[] output = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                output[i] = (char)(text[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }

        public static IEnumerable<string> GenerateKeys(int limit, string start = "", string stop = "")
        {
            string current = "";
            string final = "";

            for (int i = 0; i < limit; i++)
            {
                current += 'a';
                final += 'z';
            }

            if (start != "") current = start;
            if (stop != "") final = stop;

            yield return current;

            do 
            {
                current = current.Substring(0, limit - 1) + (char)(current[limit - 1] + 1);

                for (int i = current.Length - 1; i > 0; i--)
                {
                    if (current[i] == '{')
                    {
                        current = current.Substring(0, i - 1) + (char)(current[i - 1] + 1) + 'a' + current.Substring(i + 1);
                    }
                }

                yield return current;

            } while (current != final) ;
        }

        public static IEnumerable<string> GenerateAlphaNumKeys(int limit, string start = "", string stop = "")
        {
            string current = "";
            string final = "";
            char[] caracs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '{' };

            for (int i = 0; i < limit; i++)
            {
                current += caracs[0];
                final += caracs[caracs.Length - 2];
            }

            if (start != "") current = start;
            if (stop != "") final = stop;

            yield return current + "67";

            do
            {
                current = current.Substring(0, limit - 1) + caracs[Array.IndexOf(caracs, current[limit - 1]) + 1];

                for (int i = current.Length - 1; i > 0; i--)
                {
                    if (current[i] == '{')
                    {
                        current = current.Substring(0, i - 1) + caracs[Array.IndexOf(caracs, current[i - 1]) + 1] + caracs[0] + current.Substring(i + 1);
                    }
                }

                yield return current + "67";

            } while (current != final);
        }
    }
}