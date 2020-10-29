using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tionit.Enterprise.Exceptions;
using Tionit.MyShop.Admin.Application.Commands;
using Tionit.MyShop.Admin.Application.Commands.Administrators.Models;
using Tionit.MyShop.Admin.InteropServices;

namespace Tionit.MyShop.Admin.Pages
{
     partial class Login
    {
        #region Inject

        [Inject]
        public UserSession UserSession { get; set; }

        [Inject]
        private Executor<AuthenticateAdministratorCommand> AuthenticateAdministratorCommandExecutor { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public Blocker Blocker { get; set; }

        [Inject]
        public Messages Messages { get; set; }

        #endregion Inject

        #region Fields

        /// <summary>
        /// Логин
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        #endregion Fields

        #region Methods

        /// <summary>
        /// Вход доступен?
        /// </summary>
        public bool IsLoginAvailable => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        /// <summary>
        /// Выполняет вход в систему
        /// </summary>
        private async Task LogIn()
        {
            await Blocker.BlockPage();

            try
            {
                var input = new AuthenticateAdministratorInputModel
                {
                    UserName = Username,
                    Password = Password
                };

                var result = await AuthenticateAdministratorCommandExecutor.Execute(command => command.Execute(input));

                await UserSession.StartSession(result.UserName, result.AccessToken);

                NavigationManager.NavigateTo("/admin/admins");
            }
            catch (BusinessException exception)
            {
                await Messages.ShowError(exception.Message);
            }

            await Blocker.UnblockPage();
        }

        #endregion Methods
    }
}
