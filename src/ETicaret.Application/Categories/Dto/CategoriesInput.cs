using Abp.Application.Services.Dto;
using ETicaret.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Categories.Dto
{
    public class CategoriesInput: PagedAndFilteredInputDto
    {
        public string Name { get; set; }

    }
}
