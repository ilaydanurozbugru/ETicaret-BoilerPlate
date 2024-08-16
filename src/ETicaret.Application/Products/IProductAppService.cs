using Abp.Application.Services.Dto;
using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Products.Dto;
using ETicaret.Categories.Dto;

namespace ETicaret.Products
{
    public interface IProductAppService :  IApplicationService
    {
        Task CreateAsync(CreateProductDto input);
        Task UpdateAsync(UpdateProductDto input);
        Task DeleteAsync(EntityDto<int> input);
        Task<PagedResultDto<ProductListDto>> GetList(ProductInput input);
        Task<ProductDto> GetForEdit(EntityDto<int?> input);
    }
}