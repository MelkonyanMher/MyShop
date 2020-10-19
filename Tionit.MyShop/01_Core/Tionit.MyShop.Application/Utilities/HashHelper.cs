﻿using System.Security.Cryptography;
using System.Text;

namespace Tionit.ShopOnline.Application.Utilities
{
    /// <summary>
    /// Хеш-хелпер
    /// </summary>
    public static class HashHelper
    {
        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /// <summary>
        /// Возвращает хеш от переданной строки по алгоритму SHA1
        /// </summary>
        /// <param name="inputString">Входная строка</param>
        /// <returns></returns>
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
