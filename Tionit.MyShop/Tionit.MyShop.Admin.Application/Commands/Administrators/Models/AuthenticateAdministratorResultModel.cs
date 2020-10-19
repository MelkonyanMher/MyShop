using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Backoffice.Application.Commands.Administrators.Models
{
    public class AuthenticateAdministratorResultModel
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// e-mail пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string AccessToken { get; set; }
    }
}
