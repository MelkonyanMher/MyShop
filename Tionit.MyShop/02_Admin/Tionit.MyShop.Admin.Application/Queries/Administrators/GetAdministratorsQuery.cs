﻿using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Backoffice.Application.Queries.Administrators.Models;
using Tionit.ShopOnline.Domain;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Administrators
{
    /// <summary>
    /// возвращает всех администраторов
    /// </summary>
    public class GetAdministratorsQuery
    {
        #region Fields

        private readonly IRepository<Administrator> administratorRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly MapperConfiguration mapperConfiguration;
        
        #endregion Fields

        #region Constructor

        public GetAdministratorsQuery(IRepository<Administrator> administratorRepository, IAccessRightChecker accessRightChecker,
                                        MapperConfiguration mapperConfiguration)
        {
            this.administratorRepository = administratorRepository;
            this.accessRightChecker = accessRightChecker;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        public async Task<DataSourceResult> Execute(DataSourceRequest request)
        {
            //Проверяем права доступа
            accessRightChecker.CheckIsAdmin();
            return await administratorRepository
                .ProjectTo<AdministratorModel>(mapperConfiguration)
                .ToDataSourceResultAsync(request);
        }

        #endregion Method
    }
}
