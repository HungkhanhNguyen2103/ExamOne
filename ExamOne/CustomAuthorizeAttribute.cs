using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExamOne
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var user = context.HttpContext.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                var path = context.HttpContext.Request.Path;
                if (path.ToString() == "/") return;

                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            //var fullName = user.FindFirst("FullName")?.Value;
            //if (string.IsNullOrEmpty(fullName))
            //{
            //    context.Result = new RedirectToActionResult("Login", "Account", new { reauth = true });
            //    return;
            //}

        }

    }

    public class AuthenVerifyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user != null && user.Identity?.IsAuthenticated == true)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

        }
    }
}
