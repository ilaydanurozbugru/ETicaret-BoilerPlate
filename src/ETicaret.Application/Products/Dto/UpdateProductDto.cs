using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.Products.Dto
{
    public class UpdateProductDto : EntityDto<int>
    {
        [Required]
        [MaxLength(256)]
        public string ProductName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number")]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public Guid ImageId { get; set; }

        public string ImageToken { get; set; }
    }
}