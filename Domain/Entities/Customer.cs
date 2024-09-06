using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string CustomerName { get; set; } = null!;

        [MaxLength(150)]
        public string? CustomerSurname { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Password { get; set; } = null!;
    }
}
