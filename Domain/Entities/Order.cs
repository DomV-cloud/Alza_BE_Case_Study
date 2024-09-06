using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// 1. Číslo objednávky
    /// 2. Jméno zákazníka nebo název firmy
    /// 3. Datum vytvoření objednávky
    /// 4. Stav objednávky(Nová / Zaplacena / Zrušena)
    /// 5. Položky objednávky
    /// </summary>
    public class Order : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; } = null!;

        public Customer Customer { get; set; } = null!;

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
