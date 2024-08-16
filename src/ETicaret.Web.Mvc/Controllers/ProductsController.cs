using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers; 
using ETicaret.Products; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abp.Authorization;
using ETicaret.Authorization;

namespace ETicaret.Web.Controllers
{
    [AbpAuthorize("Pages.Products")]
    public class ProductsController : AbpController
    {
        private readonly IProductAppService _productAppService;

        public ProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        // Index: Ürünlerin listelendiği sayfa
        public async Task<ActionResult> Index()
        { 
            return View();
        }

        // GET: Create Modal View
        [AbpAuthorize(PermissionNames.Pages_Product_Update, PermissionNames.Pages_Product_Create)]
        public async Task<ActionResult> CreateOrUpdate(int? id)
        {
            var entity = await _productAppService.GetForEdit(new EntityDto<int?>(id));

            return PartialView("_CreateOrUpdate", entity);
        }


    }
}
