using System;
using System.Collections.Generic;
using System.Text;

namespace Tionit.ShopOnline.Application.Data
{
    /// <summary>
    /// Информация о фильтре
    /// </summary>
    public class FilterInfo
    {
        /// <summary>
        /// Логический оператор
        /// </summary>
        public LogicalOperator LogicalOperator{ get; set; }

        /// <summary>
        /// Дочерние фильтры
        /// </summary>
        public IEnumerable<FilterInfo> ChildFilters { get; set; }

        /// <summary>
        /// Свойство к которому применяется фильтр
        /// </summary>
        public string Member { get; set; }

        /// <summary>
        /// Оператор
        /// </summary>
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}
