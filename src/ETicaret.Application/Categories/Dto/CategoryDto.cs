﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ETicaret.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryDto : EntityDto<int>
    {
       
        public string  Name { get; set; }

    }
}
