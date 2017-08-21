using System;
using System.ComponentModel.DataAnnotations;

namespace Ak.ElVegetarianoFurio.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Filename { get; set; }

        public DateTime InvoiceDate { get; set; }

        public double Total { get; set; }


        [StringLength(100)]
        [Required]
        public string UserId { get; set; }
    }
}