using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tionit.ShopOnline.Portal.InteropServices
{
    /// <summary>
    /// Блокировщик пользовательского интерфейса
    /// </summary>
    public class Blocker
    {
        #region Fields

        private readonly IJSRuntime jsRuntime;

        #endregion Fields

        #region Constructor

        public Blocker(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Выполняет блокировку всей страницы
        /// </summary>
        public async Task BlockPage()
        {
            await jsRuntime.InvokeVoidAsync("blockPage");
        }

        /// <summary>
        /// Выполняет разблокировку страницы
        /// </summary>
        public async Task UnblockPage()
        {
            await jsRuntime.InvokeVoidAsync("unblockPage");
        }

        /// <summary>
        /// Выполняет блокировкуэлемента по селектору
        /// </summary>
        /// <param name="selector">селектор</param>
        public async Task Block(string selector)
        {
            await jsRuntime.InvokeVoidAsync("unblock", selector);
        }

        /// <summary>
        /// Выполняет разблокировку элемента по селектору
        /// </summary>
        /// <param name="selector">селектор</param>
        public async Task Unblock(string selector)
        {
            await jsRuntime.InvokeVoidAsync("unblock", selector);
        }

        #endregion Methods
    }
}
