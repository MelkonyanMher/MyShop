using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tionit.AuditLogging;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Application.Utilities;
using Tionit.MyShop.Domain;
using Tionit.Persistence;

namespace Tionit.MyShop.Admin.Application.Commands
{
    /// <summary>
    /// Создание администратора по умолчанию
    /// </summary>
    public class CreateDefaultAdminCommand
    {
        #region Fields

        private readonly IRepository<Administrator> administratorRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IChangesSaver changeSaver;
        private readonly IAuditLogger auditLogger;
        
        #endregion Fields

        #region Constructor

        public CreateDefaultAdminCommand(IRepository<Administrator> administratorRepository, IAccessRightChecker accessRightChecker,
                                         IChangesSaver changeSaver, IAuditLogger auditLogger)
        {
            this.administratorRepository = administratorRepository;
            this.accessRightChecker = accessRightChecker;
            this.changeSaver = changeSaver;
            this.auditLogger = auditLogger;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Создаем администратора по умолчанию
        /// </summary>
        public async Task Execute()
        {
            //Проверяем права доступа
            accessRightChecker.CheckIsSystem();

            //если нет ни одного администратора
            if (!await administratorRepository.AnyAsync())
            {
                Administrator defaultAdministrator = new Administrator
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    PasswordHash = HashHelper.GetHashString("admin"),
                    Email = "mkrtumyan@tionit.com",
                    AuthTokenId = Guid.NewGuid()
                };

                //добавляем администратора в репозиторию
                administratorRepository.Add(defaultAdministrator);

                //Сохраняем
                await changeSaver.SaveChangesAsync();

                //логируем
                await auditLogger
                    .Info("Создан внутренний пользователь по умолчанию")
                    .RelatedObject(defaultAdministrator)
                    .LogAsync();
            }

        }

        #endregion Method
    }
}
