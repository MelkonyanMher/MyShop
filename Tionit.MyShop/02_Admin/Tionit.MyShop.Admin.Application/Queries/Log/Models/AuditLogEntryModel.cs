﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Tionit.AuditLogging;
using Tionit.BuiltInDictionaries;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Log.Models
{
    /// <summary>
    /// Модель для записи в логе
    /// </summary>
    public class AuditLogEntryModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата и время возникновения события
        /// </summary>
        public DateTime DateAndTime { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Тип события
        /// </summary>
        public AuditLogEventType EventType { get; set; }

        /// <summary>
        /// Название типа события
        /// </summary>
        public string EventTypeName => EventType.GetName();

        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Техническая информация о пользователе
        /// </summary>
        public string UserTechnicalInformation { get; set; }

        /// <summary>
        /// подсистема
        /// </summary>
        public string Subsystem { get; set; }

        internal class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<AuditLogEntry, AuditLogEntryModel>();
            }
        }
    }
}
