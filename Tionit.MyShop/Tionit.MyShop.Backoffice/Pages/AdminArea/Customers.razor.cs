using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Backoffice.Application.Commands.Customers;
using Tionit.ShopOnline.Backoffice.Application.Commands.Customers.Models;
using Tionit.ShopOnline.Backoffice.Application.Queries.Customers;
using Tionit.ShopOnline.Backoffice.Application.Queries.Customers.Models;
using Tionit.ShopOnline.Backoffice.InteropServices;

namespace Tionit.ShopOnline.Backoffice.Pages.AdminArea
{
    public partial class Customers
    {
        #region Const

        /// <summary>
        /// Селектор грида
        /// </summary>
        private const string GridCssSelector = ".customer-grid";

        #endregion Const

        #region Inject

        [Inject]
        public Executor<GetCustomersQuery> GetCustomersQueryExecutor { get; set; }
        
        [Inject]
        public Executor<CreateCustomerCommand> CreateCustomerCommandExecutor {get; set; }
       
        [Inject]
        public Blocker Blocker { get; set; }

        [Inject]
        public Messages Messages {get; set; }

        #endregion Inject

        #region Fields

        /// <summary>
        /// Режим добавления клиента
        /// </summary>
        private bool IsCreateMode { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        private string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// Список клиентов
        /// </summary>
        private IEnumerable<CustomerModel> CustomersList { get; set; }
        
        /// <summary>
        /// Количество клиентов
        /// </summary>
        private int TotalCount { get; set; }

        #endregion Fields

        private DataSourceRequest currentGridRequest;

        #region Methods

        /// <summary>
        /// Показать кнопку сохранения?
        /// </summary>
        private bool IsSaveButtonEnabled =>
            !string.IsNullOrEmpty(UserName) &&
            !string.IsNullOrEmpty(Name);

        /// <summary>
        /// Обновление грида
        /// </summary>
        private async Task RebindGrid()
        {
            await Blocker.Block(GridCssSelector);
            
            try
            {
                var result = await GetCustomersQueryExecutor.Execute(query => query.Execute(currentGridRequest));
                CustomersList = result.Data.Cast<CustomerModel>();
                TotalCount = result.Total;
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
            
            await Blocker.Unblock(GridCssSelector);
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        private async Task CreateCustomer()
        {
            try
            {
                var input = new CreateCustomerInputModel
                {
                    Name = Name,
                    UserName = UserName
                };
                await CreateCustomerCommandExecutor.Execute(command => command.Execute(input));

                await Messages.ShowSuccess("Новый клиент успешно добавлен");


                IsCreateMode = false;
                UserName = Name = null;

            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
        }

        /// <summary>
        /// Закрываем окно добавления клиента
        /// </summary>
        private void CloseCreateWindow()
        {
            IsCreateMode = false;
            UserName = Name = null;
        }

        /// <summary>
        /// Открываем окно рдобавления клиента
        /// </summary>
        private void ShowCreateWindow()
        {
            IsCreateMode = true;
        }

        /// <summary>
        /// Возвращает всех клиентов
        /// </summary>
        private async Task OnGridNeedItems(GridReadEventArgs args)
        {
            currentGridRequest = args.Request;
            await RebindGrid();
        }

        #region Удаление и редактирование

        private async Task RemoveCustomer()
        {
            await Messages.ShowInfo("Сначала добавьте функциональность");
        }

        private async Task UpdateCustomer()
        {
            await Messages.ShowInfo("Сначала добавте функциональность");
        }

        #endregion Удаление и редактирование

        #endregion Methods
    }
}
