using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Tionit.ShopOnline.Portal.InteropServices
{
    /// <summary>
    /// Отображает сообщения через toast и swal
    /// </summary>
    public class Messages
    {
        #region Fields

        private readonly IJSRuntime jsRuntime;

        #endregion Fields

        #region Constructor

        public Messages(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Отображает сообщение об ошибке
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public async ValueTask ShowError(string message)
        {
            await jsRuntime.InvokeAsync<object>("Messages.showError", message);
        }

        /// <summary>
        /// Отображает сообщение об успехе
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public async ValueTask ShowSuccess(string message)
        {
            await jsRuntime.InvokeAsync<object>("Messages.showSuccess", message);
        }

        /// <summary>
        /// Отображает предупреждение
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public async ValueTask ShowWarning(string message)
        {
            await jsRuntime.InvokeAsync<object>("Messages.showWarning", message);
        }

        /// <summary>
        /// Отображает сообщение с информацией
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public async ValueTask ShowInfo(string message)
        {
            await jsRuntime.InvokeAsync<object>("Messages.showInfo", message);
        }

        public ValueTask<bool> ShowConfirm(string message)
        {
            return jsRuntime.InvokeAsync<bool>("Messages.showConfirm", message);
        }

        #endregion Methods
    }
}
