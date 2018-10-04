using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpyStore.MVC.Authentication;

namespace SpyStore.MVC.Filters
{
    public class AuthActionFilter : IActionFilter
    {
        private IAuthHelper authHelper;

        public AuthActionFilter(IAuthHelper authHelper)
        {
            this.authHelper = authHelper;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var viewBag = ((Controller)context.Controller).ViewBag;
            var customer = authHelper.GetCustomerInfo();
            viewBag.CustomerId = customer.Id;
            viewBag.CustomerName = customer.FullName;
        }
    }
}
