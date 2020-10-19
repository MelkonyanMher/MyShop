﻿using Tionit.Enterprise;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Application
{
    public class AccessRightChecker : IAccessRightChecker
    {
        #region Consts

        private const string AccessDeniedErrorMessage = "Недостаточно прав для выполнения операции";

        #endregion Consts

        #region Fields

        private readonly IAppUserInfoProvider userInfoProvider;


        #endregion Fields

        #region Constructor

        public AccessRightChecker(IAppUserInfoProvider userInfoProvider)
        {
            this.userInfoProvider = userInfoProvider;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Доступно только администратору
        /// </summary>
        public void CheckIsAdmin()
        {
            if (userInfoProvider.UserRole != UserRole.Admin && userInfoProvider.UserType != UserType.Authenticated)
                throw new AccessDeniedException(AccessDeniedErrorMessage);
        }

        /// <summary>
        /// Доступно только клиенту
        /// </summary>
        public void CheckIsCustomer()
        {
            if (userInfoProvider.UserRole != UserRole.Customer && userInfoProvider.UserType != UserType.Authenticated)
                throw new AccessDeniedException(AccessDeniedErrorMessage);
        }

        /// <summary>
        /// Доступно только системе
        /// </summary>
        public void CheckIsSystem()
        {
            if (userInfoProvider.UserType != UserType.System)
                throw new AccessDeniedException(AccessDeniedErrorMessage);
        }

        /// <summary>
        /// Доступно только анонимным пользователям
        /// </summary>
        public void CheckIsAnonymous()
        {
            if(userInfoProvider.UserType != UserType.Anonymous)
                throw new AccessDeniedException(AccessDeniedErrorMessage);
        }

        #endregion Methods
    }
}
