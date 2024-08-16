using ETicaret.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Products.Dto
{
    public class ProductInput:PagedAndFilteredInputDto
    {
        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public string CategoryName { get; set; }
    }
}
