﻿namespace Tionit.ShopOnline.Application.Contract.Infrastructure.MessageFormer
{
    /// <summary>
    /// Формирователь текста сообщения при создании заказа
    /// </summary>
    public interface IOrderCreationNotificationMessageFormer
    {
        /// <summary>
        /// Формирователь текста сообщения
        /// </summary>
        string Form(string customerName, string orderTotalSum, string orderName);
    }
}
