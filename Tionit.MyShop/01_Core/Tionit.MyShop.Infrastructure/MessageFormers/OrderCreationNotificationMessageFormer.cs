﻿using System.Text;
using Tionit.ShopOnline.Application.Contract.Infrastructure.MessageFormer;

namespace Tionit.ShopOnline.Infrastructure.MessageFormers
{
    public class OrderCreationNotificationMessageFormer : IOrderCreationNotificationMessageFormer
    {
        public string Form(string customerName, string orderTotalSum, string orderName)
        {
            StringBuilder messageText = new StringBuilder();

            messageText.AppendLine("Создан заказ");
            messageText.AppendLine($"Пользователь создавший заказ {customerName}");
            messageText.AppendLine($"Общая сумма заказа составляет {orderTotalSum}");
            messageText.AppendLine($"Надо придумать название заказа {orderName}");
            messageText.AppendLine(";)");

            return messageText.ToString();
        }
    }
}
