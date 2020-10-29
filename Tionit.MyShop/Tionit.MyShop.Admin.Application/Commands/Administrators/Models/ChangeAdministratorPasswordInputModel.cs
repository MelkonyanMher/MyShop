using Tionit.Enterprise.Models;
using Tionit.Enterprise.Models.Attributes;

namespace Tionit.MyShop.Admin.Application.Commands.Administrators.Models
{
    public class ChangeAdministratorPasswordInputModel: InputModelBase
    {
        /// <summary>
        /// Старый пароль
        /// </summary>
        [NotNull]
        [NotTrimable]
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        [NotNull]
        [NotTrimable]
        public string NewPassword { get; set; }

        /// <summary>
        /// Подтверждение нового пароля
        /// </summary>
        [NotNull]
        [NotTrimable]
        public string NewPasswordConfirmation { get; set; }
    }
}
