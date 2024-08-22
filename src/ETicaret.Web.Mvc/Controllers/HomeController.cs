using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ETicaret.Controllers;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using ETicaret.Products.Dto;
using ETicaret.Products;
using System.Threading.Tasks;
using ETicaret.Dashboards;

namespace ETicaret.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AbpController
    {
        private readonly IDashboardAppService _dashboardAppService;
        private readonly IProductAppService _productAppService;

        public HomeController(IProductAppService productAppService, IDashboardAppService dashboardAppService)
        {
            _productAppService = productAppService;
            _dashboardAppService = dashboardAppService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _dashboardAppService.GetList();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productAppService.GetForEdit(new EntityDto<int?> { Id = id });
            return View(product);
        }
    }
}