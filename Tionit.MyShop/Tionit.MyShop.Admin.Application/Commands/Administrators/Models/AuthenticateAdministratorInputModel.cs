using Tionit.Enterprise.Models;
using Tionit.Enterprise.Models.Attributes;

namespace Tionit.MyShop.Backoffice.Application.Commands.Administrators.Models
{
    public class AuthenticateAdministratorInputModel : InputModelBase
    {
        /// <summary>
        /// Логин
        /// </summary>
        [NotNull]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [NotTrimable]
        public string Password { get; set; }
    }
}
