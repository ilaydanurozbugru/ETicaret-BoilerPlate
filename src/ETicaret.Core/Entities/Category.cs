using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Entities
{
    [Table("Categories")]
    public class Category : Entity<int>
    {
        [Required]
        [MaxLength(128)]
        public string CategoryName { get; set; }
    }
}