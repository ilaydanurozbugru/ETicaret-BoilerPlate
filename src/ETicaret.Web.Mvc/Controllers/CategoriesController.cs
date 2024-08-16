using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using ETicaret.Authorization;
using ETicaret.Categories;
using ETicaret.Categories.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Category)]
    public class CategoriesController : AbpController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoriesController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public IActionResult Index()
        {
             

            return View();
        }

        [AbpAuthorize(PermissionNames.Pages_Category_Update,PermissionNames.Pages_Category_Create)]
        public async Task<ActionResult> CreateOrUpdate(int? id)
        {
            var category = await _categoryAppService.GetForEdit(new EntityDto<int?>(id));
             
            return PartialView("_CreateOrUpdate", category);
        }
    }
}