using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionarios
{
    class Program
    {
        static void Main(string[] args)
        {
            // Encriptar - Decriptar Metodo 1
            //// diccionario para encriptar
            Random r = new Random();
            Dictionary<string, string> eDic = new Dictionary<string, string>();
            // diccionario para decriptar
            Dictionary<string, string> dDic = new Dictionary<string, string>();
            char[] eArr = {'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','x','y','z' };
            int randomRot = r.Next(eArr.Length);
            for (int i = 0; i < eArr.Length; i++)
            {
                eDic.Add(eArr[i].ToString(), eArr[(i + randomRot) % eArr.Length].ToString());
                dDic.Add(eArr[(i + randomRot) % eArr.Length].ToString(), eArr[i].ToString());
            }
            try
            {
                Console.WriteLine("Ingrese palabara a cifrar: ");
                string s = Console.ReadLine();
                Console.WriteLine("Palabra cifrada: " + Encrypt(eDic, s));
                Console.WriteLine("Ingrese palabara a descifrar: ");
                s = Console.ReadLine();
                Console.WriteLine("Palabra descifrada: " + Decrypt(dDic, s));
            }
            catch { Console.WriteLine("Revisa tu mensaje, no ingreses espacios."); }
            Console.ReadKey();

            //Encriptar Decriptar Metodo 2(sin diccionario)
            //Console.WriteLine("Ingrese el mensaje para cifrar: ");
            //string s = Console.ReadLine();
            //Random r = new Random();
            //int seed = r.Next(65537); // c# unicode char is 16 bits
            //s = Encrypt2(s, seed);
            //Console.WriteLine("Mensaje cifrado: " + s);
            //s = Decrypt2(s, seed);
            //Console.WriteLine("Mensaje descifrado: " + s);
            //Console.ReadKey();
        }

        static string Encrypt(Dictionary<string,string> dic, string s)
        {
            string result = "";
            for(int i = 0; i < s.Length; i++)
            {
                result += dic[s[i].ToString()];
            }
            return result;
        }

        static string Decrypt(Dictionary<string, string> dic, string s)
        {
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                result += dic[s[i].ToString()];
            }
            return result;
        }

        static string Encrypt2(string s, long seed)
        {
            string result = "";
            foreach(char c in s)
            {
                int n = c;
                char c2 = (char)((n + seed) % 65536);
                result += c2;
            }
            return result;
        }

        static string Decrypt2(string s, long seed)
        {
            string result = "";
            foreach (char c in s)
            {
                int n = c;
                int c2 = (int)(Math.Abs(n - seed) % 65536);
                result += (char)c2;
            }
            return result;
        }

    }
}
