using System;
using System.ComponentModel.DataAnnotations;

namespace Ak.ElVegetarianoFurio.Models
{
    public class Coupon
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Range(1,5)]
        public int Ammount { get; set; }

        [StringLength(100)]
        [Required]
        public string UserId { get; set; }
    }
}