using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Backoffice.Application.Queries.Administrators;
using Tionit.ShopOnline.Backoffice.Application.Queries.Administrators.Models;
using Tionit.ShopOnline.Backoffice.InteropServices;

namespace Tionit.ShopOnline.Backoffice.Pages.AdminArea
{
    public partial class Administrators
    {
        #region Const

        /// <summary>
        /// Селектор грида
        /// </summary>
        private const string GridCssSelector = ".admin-grid";

        #endregion Const

        #region Inject

        [Inject]
        public Executor<GetAdministratorsQuery> GetAdministratorsQueryExecutor { get; set; }
        
        [Inject]
        public Messages Messages { get; set; }

        [Inject]
        public Blocker Blocker { get; set; }

        #endregion Inject

        #region Fields

        /// <summary>
        /// E-mail
        /// </summary>
        private string Email { get; set; }

        /// <summary>
        /// Режим редактирования администратора
        /// </summary>
        private bool IsEditMode { get; set; }

        /// <summary>
        /// Логин админа
        /// </summary>
        private string UserName { get; set; }

        /// <summary>
        /// Количество администраторов
        /// </summary>
        private int TotalCount { get; set; }


        #endregion Fields

        #region Methods

        /// <summary>
        /// Обновление администратора
        /// </summary>
        private async Task UpdateAdministrator()
        {
            UserName = Email = null;
            await Messages.ShowInfo($"Вы указали новый логин {UserName} и e-Mail {Email}  администратора \n Осатлось добавить функциональность");
            IsEditMode = false;
        }

        /// <summary>
        /// Текущие отображаемые администраторы
        /// </summary>
        private IEnumerable<AdministratorModel> AdministratorsList { get; set; }

        private void CloseUpdateWindow()
        {
            IsEditMode = false;
        }

        /// <summary>
        /// Возвращает всех администраторов
        /// </summary>
        private async Task OnGridNeedItems(GridReadEventArgs args)
        {
            await RebindGrid(args);
        }

        /// <summary>
        /// Обновляет грид
        /// </summary>
        private async Task RebindGrid(GridReadEventArgs args)
        {
            await Blocker.Block(GridCssSelector);
            
            try
            {
                var result = await GetAdministratorsQueryExecutor.Execute(query => query.Execute(args.Request));
                AdministratorsList = result.Data.Cast<AdministratorModel>();
                TotalCount = result.Total;
            }
            catch(BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }

            await Blocker.Unblock(GridCssSelector);
        }

        /// <summary>
        /// Показываеат окно добавления
        /// </summary>
        private async Task ShowCreateWindow()
        {
            await Messages.ShowInfo("Необходимо добавить функционал");
        }

        /// <summary>
        /// Активировать кнопка сохранения?
        /// </summary>
        private bool IsVisibleSaveButton => !string.IsNullOrEmpty(UserName) &&
                                            !string.IsNullOrEmpty(Email);
        /// <summary>
        /// Показать окно редактирования
        /// </summary>
        private void ShowEditWindow()
        {
            IsEditMode = true;
        }

        private async Task RemoveAdministrator()
        {
            await Messages.ShowInfo("Необходимо добавить финкционал удаления администратора");
        }

        #endregion Methods


    }
}
