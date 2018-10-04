using System.Linq;
using SpyStore.Models.Entities;
using SpyStore.MVC.WebServiceAccess.Base;

namespace SpyStore.MVC.Authentication
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IWebApiCalls webApiCalls;

        public AuthHelper(IWebApiCalls webApiCalls)
        {
            this.webApiCalls = webApiCalls;
        }

        public Customer GetCustomerInfo()
        {
            return webApiCalls.GetCustomersAsync().Result.FirstOrDefault();
        }
    }
}
