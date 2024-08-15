using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using ETicaret.Products.Dto;
using ETicaret.Products;
using ETicaret.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abp.Authorization;

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
        public async Task<ActionResult> Index(PagedProductResultRequestDto input)
        {
            var result = await _productAppService.GetAllAsync(input);

            var model = new ProductListViewModel
            {
                Products = result.Items,
                TotalCount = result.TotalCount
            };

            return View(model);
        }

        // GET: Create Modal View
        public ActionResult CreateModal()
        {
            var model = new ProductCreateViewModel
            {
                Product = new CreateProductDto()
            };

            return PartialView("_CreateModal", model);
        }

        // POST: Create New Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductDto input)
        {
            if (ModelState.IsValid)
            {
                await _productAppService.CreateAsync(input);
                return RedirectToAction("Index");
            }

            // Eğer model valid değilse, formu geri döndürürüz.
            var model = new ProductCreateViewModel
            {
                Product = input
            };

            return View("CreateModal", model);
        }

        // GET: Edit Modal View
        public async Task<ActionResult> EditModal(int productId)
        {
            var product = await _productAppService.GetAsync(new EntityDto<int>(productId));
            var model = new EditProductModalViewModel
            {
                Product = product
            };

            return PartialView("_EditModal", model);
        }

        // POST: Delete Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _productAppService.DeleteAsync(new EntityDto<int>(id));
            return RedirectToAction("Index");
        }

        // JSON: Get All Products (For DataTables or AJAX)
        public async Task<JsonResult> GetAllProducts()
        {
            var result = await _productAppService.GetAllAsync(new PagedProductResultRequestDto());
            return Json(new
            {
                data = result.Items
            });
        }
    }
}
