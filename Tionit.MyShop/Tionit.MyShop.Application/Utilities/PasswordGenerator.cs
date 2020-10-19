using System;
using System.Collections.Generic;
using System.Text;

namespace Tionit.ShopOnline.Application.Utilities
{
    public class PasswordGenerator
    {
        /// <summary>
        /// Генерирует пароль указанной длины
        /// </summary>
        /// <param name="length">Длина пароля</param>
        /// <returns>Возвращает случайный пароль</returns>
        public static string GeneratorPassword(int length)
        {
            const string charSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; //источник симжолож для пароля
            StringBuilder passwordBuilder = new StringBuilder();
            Random rnd = new Random();
            for (int charIndex = 0; charIndex < length; ++charIndex)
                passwordBuilder.Append(charSource[rnd.Next(charSource.Length)]);
            return passwordBuilder.ToString();
        }
    }
}
