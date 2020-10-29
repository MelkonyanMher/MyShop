namespace Tionit.MyShop.Admin.Application.Commands.Customers.Models
{
    /// <summary>
    /// Возвращаемая модел клиента
    /// </summary>
    public class CustomerResultModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// пароль
        /// </summary>
        public string Password { get; set; }
    }
}
