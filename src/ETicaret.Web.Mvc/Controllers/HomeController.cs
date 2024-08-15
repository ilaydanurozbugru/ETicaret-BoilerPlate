using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ETicaret.Controllers;

namespace ETicaret.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ETicaretControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
