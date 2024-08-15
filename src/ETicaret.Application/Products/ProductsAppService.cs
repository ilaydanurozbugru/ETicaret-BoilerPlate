using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ETicaret.Entities;
using ETicaret.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Products
{

    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, int, PagedProductResultRequestDto, CreateProductDto, UpdateProductDto>, IProductAppService
    {
        public ProductAppService(IRepository<Product, int> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Product> CreateFilteredQuery(PagedProductResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.ProductName.Contains(input.Keyword) || x.Description.Contains(input.Keyword))
                .WhereIf(input.MinPrice.HasValue, x => x.Price >= input.MinPrice.Value)
                .WhereIf(input.MaxPrice.HasValue, x => x.Price <= input.MaxPrice.Value);
        }
    }
}