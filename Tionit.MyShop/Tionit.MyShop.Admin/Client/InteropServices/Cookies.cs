using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Tionit.MyShop.Admin.InteropServices
{
    public class Cookies
    {
        #region Fields

        private readonly IJSRuntime jsRuntime;

        #endregion Fields

        #region Constructor

        public Cookies(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Устанавливает куку
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">значение</param>
        public async Task SetCookies(string key, string value)
        {
            await jsRuntime.InvokeAsync<object>("Cookies.set", key, value);
        }

        /// <summary>
        /// Удаляет куку
        /// </summary>
        /// <param name="key">ключ</param>
        public async Task RemoveCookies(string key)
        {
            await jsRuntime.InvokeAsync<object>("Cookies.remove", key);
        }

        /// <summary>
        /// Достаёт куку
        /// </summary>
        /// <param name="key">ключ</param>
        public async Task<string> GetCookies(string key)
        {
            return await jsRuntime.InvokeAsync<string>("Cookies.get", key);
        }

        #endregion Methods
    }
}
