using System;
using System.Security.Cryptography;
using System.Text;

namespace FuelStantion
{
    class Utils
    {
        public static bool AreEqual(double lhs, double rhs)
        {
            return Math.Abs(rhs - lhs) < 0.001;
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
