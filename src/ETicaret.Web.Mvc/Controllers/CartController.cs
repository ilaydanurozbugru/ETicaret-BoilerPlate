using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Web.Controllers
{
    public class CartController : AbpController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
