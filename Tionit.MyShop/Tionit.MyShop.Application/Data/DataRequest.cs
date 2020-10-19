using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Telerik.DataSource;

namespace Tionit.ShopOnline.Application.Data
{
    public class DataRequest
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Сортировка
        /// </summary>
        public ICollection<SortInfo> Sorts { get; set; } = new List<SortInfo>();

        /// <summary>
        /// Фильтры
        /// </summary>
        public ICollection<FilterInfo> Filters { get; set; } = new List<FilterInfo>();

        /// <summary>
        /// Превращает запрос данных в запрос от Telerik
        /// </summary>
        public DataSourceRequest ToTelerikDataSourceRequest()
        {
            DataSourceRequest result = new DataSourceRequest
            {
                Page = Page,
                PageSize = PageSize,
                Filters = new List<IFilterDescriptor>(),
                Sorts = new List<SortDescriptor>()
            };

            //Фильтры
            void ProcessFilterInfo(FilterInfo filterInfo, IList<IFilterDescriptor> distCollection)
            {
                if (filterInfo.ChildFilters.Any())
                {
                    CompositeFilterDescriptor compositeFilterDescriptor = new CompositeFilterDescriptor
                    {
                        LogicalOperator = filterInfo.LogicalOperator == LogicalOperator.And
                                          ?FilterCompositionLogicalOperator.And
                                          :FilterCompositionLogicalOperator.Or
                    };
                    distCollection.Add(compositeFilterDescriptor);

                    foreach(FilterInfo filterInfoChildFilter in filterInfo.ChildFilters)
                        ProcessFilterInfo(filterInfoChildFilter, compositeFilterDescriptor.FilterDescriptors);
                }
                else if (!string.IsNullOrEmpty(filterInfo.Member))
                {
                    FilterDescriptor filterDescriptor = 
                        new FilterDescriptor(filterInfo.Member, (Telerik.DataSource.FilterOperator)filterInfo.Operator,
                            JsonConvert.DeserializeObject(filterInfo.Value));
                }
            }

            foreach(FilterInfo filterInfo in Filters)
                ProcessFilterInfo(filterInfo, result.Filters);

            foreach (SortInfo sortInfo in Sorts)
            {
                SortDescriptor sortDescriptor = new SortDescriptor(sortInfo.Member,
                    sortInfo.Direction == SortDirection.Ascending
                    ? ListSortDirection.Ascending
                    : ListSortDirection.Descending);
                result.Sorts.Add(sortDescriptor);
            }

            return result;
        }
    }
}
