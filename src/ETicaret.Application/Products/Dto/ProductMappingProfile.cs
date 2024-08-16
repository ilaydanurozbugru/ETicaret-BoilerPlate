using AutoMapper;
using ETicaret.Entities;
using ETicaret.Products.Dto;

namespace ETicaret.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // ProductDto ile UpdateProductDto arasında bir eşleme (mapping) tanımlanıyor
            CreateMap<Product, ProductListDto>(); 
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Eğer ters eşleme gerekiyorsa, aşağıdaki satırı da ekleyin
            // CreateMap<UpdateProductDto, ProductDto>();
        }
    }
}