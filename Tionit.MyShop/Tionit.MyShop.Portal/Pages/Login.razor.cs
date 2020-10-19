using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Portal.Application.Command.Customers;
using Tionit.ShopOnline.Portal.Application.Command.Customers.Models;
using Tionit.ShopOnline.Portal.InteropServices;

namespace Tionit.ShopOnline.Portal.Pages
{
    partial class Login
    {
        #region Inject
        
        [Inject]
        public UserSession UserSession { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public Blocker Blocker { get; set; }

        [Inject]
        public Executor<AuthenticateCustomerCommand> AuthenticateCustomerCommandExecutor { get; set; }

        [Inject]
        public Messages Messages { get; set; }
        #endregion Inject

        #region Fields

        /// <summary>
        /// Логин
        /// </summary>
        private string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        private string Password { get; set; }

        #endregion Fields

        #region Methods

        /// <summary>
        /// Вход доступен?
        /// </summary>
        private bool IsLoginAvailable => !string.IsNullOrEmpty(UserName) &&
                                         !string.IsNullOrEmpty(Password);       

        private async Task LogIn()
        {
            await Blocker.BlockPage();

            try
            {
                var input = new AuthenticateCustomerInputModel
                {
                    UserName = UserName,
                    Password = Password
                };
                var result = await AuthenticateCustomerCommandExecutor.Execute(command => command.Execute(input));

                await UserSession.StartSession(result.UserName, result.AccessToken);

                NavigationManager.NavigateTo("/customer/products");
            }
            catch(BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
            
            await Blocker.UnblockPage();
        }

        #endregion Methods
    }
}
