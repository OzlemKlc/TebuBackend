using Microsoft.AspNetCore.Authentication.Cookies;
using Tebu.API.Service;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Tebu.API.Events
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {

        private CurrentUserService currentUserService;

        public CustomCookieAuthenticationEvents(CurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var id = (from c in userPrincipal.Claims
                      where c.Type == ClaimTypes.NameIdentifier
                      select c.Value).SingleOrDefault();

            bool success = false;

            if (id != null)
            {
                success = currentUserService.TryToSetCurrentUser(id);
            }

            if (!success)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
