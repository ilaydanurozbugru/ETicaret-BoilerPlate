using AutoMapper;
using ETicaret.Categories.Dto;
using ETicaret.Common.Dto;
using ETicaret.Entities;
using ETicaret.Products.Dto;

namespace ETicaret.Products
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // ProductDto ile UpdateProductDto arasında bir eşleme (mapping) tanımlanıyor
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, ReferanceDto<int>>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Eğer ters eşleme gerekiyorsa, aşağıdaki satırı da ekleyin
            // CreateMap<UpdateProductDto, ProductDto>();
        }
    }
}