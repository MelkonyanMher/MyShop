using Tionit.Enterprise.Models;
using Tionit.Enterprise.Models.Attributes;

namespace Tionit.MyShop.Backoffice.Application.Commands.Customers.Models
{
    /// <summary>
    /// Модель добавления клиента
    /// </summary>
    public class CreateCustomerInputModel : InputModelBase
    {
        /// <summary>
        /// Логин
        /// </summary>
        [NotNull]
        public string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [NotNull]
        public string Name { get; set; }
    }
}
