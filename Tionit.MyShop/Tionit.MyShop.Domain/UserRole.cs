using Tionit.BuiltInDictionaries;

namespace Tionit.ShopOnline.Domain
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Администратор
        /// </summary>
        [Name("Администратор")]
        Admin = 1,

        /// <summary>
        /// Клиент
        /// </summary>
        [Name("Клиент")]
        Customer = 2
    }
}
