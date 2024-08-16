using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ETicaret.Authorization;
using ETicaret.Categories.Dto;
using ETicaret.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Categories
{
    [AbpAuthorize(PermissionNames.Pages_Category)]
    public class CategoryAppService : ETicaretAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category, int> _categoryRepository;

        public CategoryAppService(IRepository<Category, int> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResultDto<CategoryListDto>> GetList(CategoriesInput input)
        {
            var categories = _categoryRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Name), x => x.Name.ToLower().Contains(input.Name.ToLower()));
            var count = await categories.CountAsync();
            var items = await categories.PageBy(input).ToListAsync();
            return new PagedResultDto<CategoryListDto>(count, ObjectMapper.Map<List<CategoryListDto>>(categories));
        }

        [AbpAuthorize(PermissionNames.Pages_Category_Update, PermissionNames.Pages_Category_Create)]
        public async Task<CategoryDto> GetForEdit(EntityDto<int?> input)
        {
            if (input.Id.HasValue)
            {
                var category = await _categoryRepository.GetAsync(input.Id.Value);
                return ObjectMapper.Map<CategoryDto>(category);
            }
           return new CategoryDto();
        }

        [AbpAuthorize(PermissionNames.Pages_Category_Create)]
        public async Task CreateAsync(CreateCategoryDto input)
        {
            var category = ObjectMapper.Map<Category>(input);
            await _categoryRepository.InsertAsync(category);
        }

        [AbpAuthorize(PermissionNames.Pages_Category_Update)]
        public async Task UpdateAsync(UpdateCategoryDto input)
        {
            var category = await _categoryRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, category);
        }

        [AbpAuthorize(PermissionNames.Pages_Category_Delete)]
        public async Task DeleteAsync(EntityDto<int> input)
        {
            
            await _categoryRepository.DeleteAsync(input.Id);
        }
    }

}