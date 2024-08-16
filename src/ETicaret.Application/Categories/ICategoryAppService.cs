using Abp.Application.Services.Dto;
using Abp.Application.Services;
using ETicaret.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        
        Task CreateAsync(CreateCategoryDto input);
        Task UpdateAsync(UpdateCategoryDto input);
        Task DeleteAsync(EntityDto<int> input);
        Task<PagedResultDto<CategoryListDto>> GetList(CategoriesInput input);
        Task<CategoryDto> GetForEdit(EntityDto<int?> input);
    }

}