using ETicaret.Dashboards.Dto;
using ETicaret.Products;
using ETicaret.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Dashboards
{
  public class DashboardAppService : ETicaretAppServiceBase, IDashboardAppService

    {
        private readonly IProductAppService _productRepository;

        public DashboardAppService (IProductAppService productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<DashboardsDto> GetList()
        {
            var dto = new DashboardsDto
            {
                ProductList = await _productRepository.GetList(new ProductInput ())        
            };
            return dto;
        }

    }
}
