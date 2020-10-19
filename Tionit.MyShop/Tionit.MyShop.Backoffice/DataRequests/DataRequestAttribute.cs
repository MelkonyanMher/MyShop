using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tionit.ShopOnline.Backoffice.DataRequests
{
    public class DataRequestAttribute : ModelBinderAttribute
    {
        public DataRequestAttribute()
        {
            BinderType = typeof(DataRequestModelBinder);
        }

        public override BindingSource BindingSource => BindingSource.Query;
    }
}
