using System.ComponentModel.DataAnnotations;

namespace Ak.ElVegetarianoFurio.Models
{
    public class DeliveryAddress
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(80)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(80)]
        public string Street { get; set; }
        [Required]
        [MaxLength(8)]
        public string Zip { get; set; }
        [Required]
        [MaxLength(80)]
        public string City { get; set; }

        [StringLength(100)]
        [Required]
        public string UserId { get; set; }
    }
}