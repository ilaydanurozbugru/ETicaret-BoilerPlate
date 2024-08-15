using Abp.Application.Services.Dto;
using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Products.Dto;

namespace ETicaret.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, int, PagedProductResultRequestDto, CreateProductDto, UpdateProductDto>
    {
        Task<PagedResultDto<ProductDto>> GetAllAsync(PagedProductResultRequestDto input);
    }
}