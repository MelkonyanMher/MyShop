using Tionit.Enterprise.Models;
using Tionit.Enterprise.Models.Attributes;

namespace Tionit.MyShop.Portal.Application.Command.Orders.Models
{
    /// <summary>
    /// Модель для создания заказа
    /// </summary>
    public class CreateOrderInputModel : InputModelBase
    {
        /// <summary>
        /// Адрес доставки товара
        /// </summary>
        [NotNull]
        public string Address { get; set; }
    }
}
