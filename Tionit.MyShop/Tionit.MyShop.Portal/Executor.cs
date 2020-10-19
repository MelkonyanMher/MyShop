using Autofac;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Tionit.AuditLogging;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Application.Contract;

namespace Tionit.ShopOnline.Portal
{
    /// <summary>
    /// Палач
    /// </summary>
    /// <typeparam name="T">тип команды или запроса</typeparam>
    public class Executor<T>
    {
        #region Constructor

        public Executor(ILifetimeScope autofacLifetimeScope, IHostEnvironment hostEnvironment)
        {
            this.autofacLifetimeScope = autofacLifetimeScope;
            this.hostEnvironment = hostEnvironment;
        }

        #endregion Constructor

        #region Fields

        private readonly ILifetimeScope autofacLifetimeScope;
        private readonly IHostEnvironment hostEnvironment;

        #endregion Fields

        /// <summary>
        /// Выполняет казнь осуждённого
        /// </summary>
        /// <typeparam name="TResult">тип команды или запроса</typeparam>
        /// <param name="executionFunc">func, выполняющий команду/запрос</param>
        public async Task<TResult> Execute<TResult>(Func<T, Task<TResult>> executionFunc)
        {
            var userInfoProvider = autofacLifetimeScope.Resolve<IAppUserInfoProvider>();

            using var scope = autofacLifetimeScope.BeginLifetimeScope();
            var userInfoSetter = scope.Resolve<IAppUserInfoSetter>();
            SetUserInfo(userInfoSetter, userInfoProvider);

            var commandOrQuery = scope.Resolve<T>();
            try
            {
                return await executionFunc(commandOrQuery);
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception exception)
            {
                IAuditLogger logger = scope.Resolve<IAuditLogger>();
                logger.Error(exception);

                // в dev-среде выбрасываем исключение в браузер
                if (hostEnvironment.IsDevelopment())
                    throw;

                throw new BusinessException("Произошла внутренняя ошибка системы");
            }
        }

        /// <summary>
        /// Выполняет казнь осуждённого
        /// </summary>
        /// <param name="executionFunc">func, выполняющий команду/запрос</param>
        public async Task Execute(Func<T, Task> executionFunc)
        {
            var userInfoProvider = autofacLifetimeScope.Resolve<IAppUserInfoProvider>();

            using (var scope = autofacLifetimeScope.BeginLifetimeScope())
            {
                var userInfoSetter = scope.Resolve<IAppUserInfoSetter>();
                SetUserInfo(userInfoSetter, userInfoProvider);

                var commandOrQuery = scope.Resolve<T>();
                try
                {
                    await executionFunc(commandOrQuery);
                }
                catch (BusinessException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    IAuditLogger logger = scope.Resolve<IAuditLogger>();
                    logger.Error(exception);

                    // в dev-среде выбрасываем исключение в браузер
                    if (hostEnvironment.IsDevelopment())
                        throw;

                    throw new BusinessException("Произошла внутренняя ошибка системы");
                }
            }
        }

        private void SetUserInfo(IAppUserInfoSetter userInfoSetter, IAppUserInfoProvider userInfoProvider)
        {
            userInfoSetter.UserName = userInfoProvider.UserName;
            //userInfoSetter.OriginalUser = userInfoProvider.OriginalUser;
            userInfoSetter.UserRole = userInfoProvider.UserRole;
            userInfoSetter.UserTechnicalInfo = userInfoProvider.UserTechnicalInfo;
            //userInfoSetter.UserTokenId = userInfoProvider.UserTokenId;
            userInfoSetter.UserType = userInfoProvider.UserType;
        }
    }
}
