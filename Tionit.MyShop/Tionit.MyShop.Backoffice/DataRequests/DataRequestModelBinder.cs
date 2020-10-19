using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Tionit.ShopOnline.Application.Data;

namespace Tionit.ShopOnline.Backoffice.DataRequests
{
    public class DataRequestModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            DataRequest dataRequest = new DataRequest();

            // page
            ValueProviderResult pageResult = bindingContext.ValueProvider.GetValue("Page");
            dataRequest.Page = int.Parse(pageResult.FirstValue);

            // page size
            ValueProviderResult pageSizeResult = bindingContext.ValueProvider.GetValue("PageSize");
            dataRequest.PageSize = int.Parse(pageSizeResult.FirstValue);

            // сортировка
            ValueProviderResult sortsResult = bindingContext.ValueProvider.GetValue("Sorts");
            foreach (string value in sortsResult)
            {
                SortInfo sortInfo = JsonConvert.DeserializeObject<SortInfo>(value);
                dataRequest.Sorts.Add(sortInfo);
            }

            // филтры
            ValueProviderResult filtersResult = bindingContext.ValueProvider.GetValue("Filters");
            foreach (string value in filtersResult)
            {
                FilterInfo filterInfo = JsonConvert.DeserializeObject<FilterInfo>(value);
                dataRequest.Filters.Add(filterInfo);
            }

            bindingContext.Result = ModelBindingResult.Success(dataRequest);

            return Task.CompletedTask;
        }
    }
}
