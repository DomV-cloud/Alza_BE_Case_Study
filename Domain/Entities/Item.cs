using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// 1. Název zboží
    /// 2. Počet kusů zboží
    /// 3. Cenu za jeden kus zboží
    /// </summary>
    public class Item : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; } = null!;

        [Required]
        public int NumberOfItems { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Price can be only positive number.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
