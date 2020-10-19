using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tionit.ShopOnline.Portal.Application.Command.Orders;
using Tionit.ShopOnline.Portal.Application.Command.Orders.Models;
using Tionit.ShopOnline.Portal.Application.Queries.Baskets;
using Tionit.ShopOnline.Portal.Application.Queries.Baskets.Models;
using Tionit.ShopOnline.Portal.InteropServices;
using Tionit.Enterprise.Exceptions;
using Tionit.AuditLogging;
using System.Linq;

namespace Tionit.ShopOnline.Portal.Pages.CustomerArea
{
    public partial class Basket
    {
        #region Inject

        [Inject]
        public Executor<GetProductsFromBasketQuery> GetProductsFromBasketQueryExecutor { get; set; }

        [Inject]
        public Messages Messages { get; set; }

        [Inject]
        public Executor<CreateOrderCommand> CreateOrderCommandExecutor { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuditLogger auditLogger { get; set; }

        #endregion Inject

        #region Fields
        
        /// <summary>
        /// Список моделей товаров в корзине
        /// </summary>
        private List<ProductModelForBasket> ProductModelForBasketList { get; set; } = new List<ProductModelForBasket>();

        /// <summary>
        /// Количество позициий товаров в корзине
        /// </summary>
        private int Count { get; set; }
               

        /// <summary>
        /// Сумма заказа
        /// </summary>
        private double TotalSum { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        private string Address { get; set; }

        #endregion Fields

        #region Methods    

        /// <summary>
        /// начальная загрузка товаров в корзину
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await ReBind();
        }

        /// <summary>
        /// обновление списка продуктов в корзине
        /// </summary>
        private async Task ReBind()
        {
            try
            {
                ProductModelForBasketList = await GetProductsFromBasketQueryExecutor.Execute(query => query.Execute());
                Count = ProductModelForBasketList.Count;
                TotalSum = ProductModelForBasketList.Sum(p => p.Price * p.Quantity);
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        private async Task CreateOrder()
        {
            try
            {
                var input = new CreateOrderInputModel
                {
                    Address = Address
                };
                await CreateOrderCommandExecutor.Execute(comman => comman.Execute(input));

                await Messages.ShowSuccess("Заказ успешно создан");
                
                await ReBind();              
            }
            catch(BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
            catch(PossibleBugException possibleBugException)
            {
                await Messages.ShowError("Произошла внутренняя ошибка");
                
                await auditLogger
                    .Error("Во время добавления заказа было указано невалидное значение адреса доставки")
                    .Exception(possibleBugException)
                    .LogAsync();
            }
        }

        /// <summary>
        /// Переходит на станицу товаров
        /// </summary>
        private void GoProductPage()
        {
            NavigationManager.NavigateTo("/customer/products");
        }

        #endregion Methods
    }    
}