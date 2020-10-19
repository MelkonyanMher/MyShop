﻿using Tionit.Enterprise;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Persistence
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="TEntity">тип сущности, за хранение которой отвечает репозиторий</typeparam>
    public class Repository<TEntity>  : Repository<AppDbContext, TEntity> where TEntity : class, IEntityWithId
    {
        public Repository(AppDbContext context) : base(context)
        {
        }
    }
}
