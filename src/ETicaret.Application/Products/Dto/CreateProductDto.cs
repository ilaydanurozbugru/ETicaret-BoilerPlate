using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.Products.Dto
{
    public class CreateProductDto : EntityDto<int>
    {
        [Required]
        [MaxLength(256)]
        public string ProductName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan fazla olmalıdır")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number")]
        public int StockQuantity { get; set; }

    }
}