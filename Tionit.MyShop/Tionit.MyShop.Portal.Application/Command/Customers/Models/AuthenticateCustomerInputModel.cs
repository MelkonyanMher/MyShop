using Tionit.Enterprise.Models;
using Tionit.Enterprise.Models.Attributes;

namespace Tionit.ShopOnline.Portal.Application.Command.Customers.Models
{
    public class AuthenticateCustomerInputModel : InputModelBase
    {
        /// <summary>
        /// Логин клиента
        /// </summary>
        [NotNull]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль клиента
        /// </summary>
        [NotNull]
        [NotTrimable]
        public string Password { get; set; }
    }
}
