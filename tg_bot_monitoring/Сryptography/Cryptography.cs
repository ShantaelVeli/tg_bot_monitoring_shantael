using System.IO;

namespace Cryptography
{
    public class Aes
    {
        public static void Encrypt(string ssh)
        {
            var strKey = File.ReadAllLines("./aes/aes-key.txt");
            Console.WriteLine(strKey[0]);
            Console.WriteLine(strKey[1]);
        }
    }
}