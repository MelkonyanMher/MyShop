using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Tionit.ShopOnline.Application.AuthOptions;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Backoffice
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return AdminTokenValidator.Validate(context.GetHttpContext());
        }
    }
}