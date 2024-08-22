using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.Internal.Mappers;
using ETicaret.Authorization;
using ETicaret.Categories.Dto;
using ETicaret.Common.Dto;
using ETicaret.Entities;
using ETicaret.Products.Dto;
using ETicaret.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Products
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductAppService : ETicaretAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product> _prodcutRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IStorageAppService _storageAppService;
        public ProductAppService(IRepository<Product> productRepository,
                                 IStorageAppService storageAppService,
                                 IRepository<Category> categoryRepository)
        {
            _prodcutRepository = productRepository;
            _categoryRepository = categoryRepository;
            _storageAppService = storageAppService;
        }

        public async Task<PagedResultDto<ProductListDto>> GetList(ProductInput input)
        {
            var query = _prodcutRepository.GetAll().Include(x => x.Category)
                .WhereIf(!string.IsNullOrEmpty(input.Description), x => x.Description.ToLower().Contains(input.Description.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(input.ProductName), x => x.ProductName.ToLower().Contains(input.ProductName.ToLower()))
                .WhereIf(input.Price.HasValue, x => x.Price == input.Price)
                .WhereIf(input.StockQuantity.HasValue, x => x.StockQuantity == input.StockQuantity)
                .WhereIf(!string.IsNullOrEmpty(input.CategoryName), x => x.Category.Name.ToLower().Contains(input.CategoryName.ToLower()));
            var count = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();
            return new PagedResultDto<ProductListDto>(count, ObjectMapper.Map<List<ProductListDto>>(items));
        }


        [AbpAuthorize(PermissionNames.Pages_Product_Update, PermissionNames.Pages_Product_Create)]
        public async Task<ProductDto> GetForEdit(EntityDto<int?> input)
        {
            if (input.Id.HasValue)
            {
                var category = ObjectMapper.Map<ProductDto>(await _prodcutRepository.GetAsync(input.Id.Value));
                category.CategoryList = await ObjectMapper.ProjectTo<ReferanceDto<int>>(_categoryRepository.GetAll().OrderBy(x => x.Name)).ToListAsync();
                return ObjectMapper.Map<ProductDto>(category);
            }
            return new ProductDto
            {
                CategoryList = await ObjectMapper.ProjectTo<ReferanceDto<int>>(_categoryRepository.GetAll().OrderBy(x => x.Name)).ToListAsync()
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Product_Create)]
        public async Task CreateAsync(CreateProductDto input)
        {
            var category = ObjectMapper.Map<Product>(input);
            if (!string.IsNullOrEmpty(input.ImageToken))
            {
                category.ImageId = await _storageAppService.UpdateFile(input.ImageToken, null);
            }
            await _prodcutRepository.InsertAsync(category);
        }

        [AbpAuthorize(PermissionNames.Pages_Product_Update)]
        public async Task UpdateAsync(UpdateProductDto input)
        {
            var category = await _prodcutRepository.GetAsync(input.Id);
            if (!string.IsNullOrEmpty(input.ImageToken))
            {
                category.ImageId = await _storageAppService.UpdateFile(input.ImageToken, category.ImageId);
            }
            ObjectMapper.Map(input, category);
        }

        [AbpAuthorize(PermissionNames.Pages_Product_Delete)]
        public async Task DeleteAsync(EntityDto<int> input)
        {

            await _prodcutRepository.DeleteAsync(input.Id);
        }
    }
}