using Telerik.Blazor.Services;
using Tionit.MyShop.Admin.Resources;

namespace Tionit.MyShop.Admin.InteropServices
{
    public class Localizer : ITelerikStringLocalizer
    {
        public string this[string name] => GetStringFromResource(name);

        public string GetStringFromResource(string key)
        {
            return TelerikMessages.ResourceManager.GetString(key, TelerikMessages.Culture);
        }
    }
}
