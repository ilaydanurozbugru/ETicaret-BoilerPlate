using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Storage;

namespace ETicaret.Entities
{
    [Table("Products")]
    public class Product : FullAuditedEntity<int>
    {
        [Required]
        [MaxLength(256)]
        public string ProductName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Guid ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public BinaryObject BinaryObject { get; set; }
    }
}