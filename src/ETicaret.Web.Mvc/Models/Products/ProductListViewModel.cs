using ETicaret.Products.Dto;
using ETicaret.Roles.Dto;
using System.Collections.Generic;

namespace ETicaret.Web.Models.Products
{
    public class ProductListViewModel
    {
        public IReadOnlyList<ProductDto> Products { get; set; }
        public int TotalCount { get; set; }
    }
}