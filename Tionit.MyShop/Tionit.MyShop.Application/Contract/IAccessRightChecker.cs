namespace Tionit.MyShop.Application.Contract
{
    /// <summary>
    /// Компонент для проверки прав доступа
    /// </summary>
    public interface IAccessRightChecker
    {
        /// <summary>
        /// Проверяет, что текущий пользователь является админом
        /// </summary>
        void CheckIsAdmin();

        /// <summary>
        /// Проверяет, что текущий пользователь является клиентом
        /// </summary>
        void CheckIsCustomer();

        /// <summary>
        /// Проверяет, что текущий пользователь является система
        /// </summary>
        void CheckIsSystem();

        void CheckIsAnonymous();
    }
}
