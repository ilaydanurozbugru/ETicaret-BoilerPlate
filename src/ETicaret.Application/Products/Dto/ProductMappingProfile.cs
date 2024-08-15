using AutoMapper;
using ETicaret.Products.Dto;

namespace ETicaret.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // ProductDto ile UpdateProductDto arasında bir eşleme (mapping) tanımlanıyor
            CreateMap<ProductDto, UpdateProductDto>();

            // Eğer ters eşleme gerekiyorsa, aşağıdaki satırı da ekleyin
            // CreateMap<UpdateProductDto, ProductDto>();
        }
    }
}