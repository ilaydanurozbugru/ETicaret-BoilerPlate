using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Categories.Dto
{
    public class CreateCategoryDto : EntityDto<int>  
    {
        [Required]
        [MaxLength(256)]
        public string  Name { get; set; }
    }
}

