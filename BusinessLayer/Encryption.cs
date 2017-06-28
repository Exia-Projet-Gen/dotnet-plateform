using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Encryption
    {
        public static string Process(string message, string key)
        {
            List<byte> messageBytes = System.Text.Encoding.UTF8.GetBytes(message).ToList();
            List<byte> keyBytes = System.Text.Encoding.UTF8.GetBytes(key).ToList();
            List<byte> processed = new List<byte>();

            for(int i = 0; i < messageBytes.Count; i++)
            {
                processed.Add((byte)(messageBytes[i] ^ keyBytes[i % keyBytes.Count]));
            }

            return System.Text.Encoding.UTF8.GetString(processed.ToArray());
        }

        public static IEnumerable<string> GenerateKeys(int limit)
        {
            string current = "";
            string final = "";

            for (int i = 0; i < limit; i++)
            {
                current += 'a';
                final += 'z';
            }

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
    }
}
