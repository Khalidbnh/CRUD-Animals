using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication9.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the session variable "Username" exists
            var session = context.HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(session))
            {
                // Redirect to the Login page if the session is not set
                context.Result = new RedirectToActionResult("Login", "Authentication", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
