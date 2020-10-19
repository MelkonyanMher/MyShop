using Telerik.Blazor.Services;
using Tionit.ShopOnline.Backoffice.Resources;

namespace Tionit.ShopOnline.Backoffice.InteropServices
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
