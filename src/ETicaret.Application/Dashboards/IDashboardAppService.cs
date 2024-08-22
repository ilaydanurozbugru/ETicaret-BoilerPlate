using ETicaret.Dashboards.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Dashboards
{
    public interface IDashboardAppService
    {
        Task<DashboardsDto> GetList();
    }

}
