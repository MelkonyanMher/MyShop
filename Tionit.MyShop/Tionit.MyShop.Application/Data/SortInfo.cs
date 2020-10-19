namespace Tionit.MyShop.Application.Data
{
    /// <summary>
    /// Информация о сортировке
    /// </summary>
    public class SortInfo
    {
        /// <summary>
        /// Направление сортировки
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Свойство по которому осуществляется сортировка
        /// </summary>
        public string Member { get; set; }
    }
}
