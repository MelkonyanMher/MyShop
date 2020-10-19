﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Tionit.ShopOnline.Application.AuthOptions
{
    /// <summary>
    /// Опции авторизации по jwt-токену для админки
    /// </summary>
    public class CustomerAuthOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public const string Issuer = "OSH_User";

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public const string Audience = "OSH_User";

        /// <summary>
        /// Ключь для шифрования
        /// </summary>
        public const string Key = "OSH_User_ff8f*038(12s14s5shjp[(dnmde$g{fvdov+7f7";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
