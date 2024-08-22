using Abp.Application.Services.Dto;
using ETicaret.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Dashboards.Dto
{
    public class DashboardsDto
    {
        public PagedResultDto<ProductListDto> ProductList { get; set; }
    }
}
