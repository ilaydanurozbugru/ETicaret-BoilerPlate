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
    
    public class Category : Entity<int>
    {
        public string  Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}