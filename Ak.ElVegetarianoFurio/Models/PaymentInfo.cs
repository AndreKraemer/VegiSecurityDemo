using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ak.ElVegetarianoFurio.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }

        [Required]
        public CreditCardType CardType { get; set; }

        [StringLength(20)]
        [Required]
        public string AccountNumber { get; set; }

        [StringLength(8)]
        [Required]
        public string ExpirationDate { get; set; }

        [StringLength(3)]
        [Required]
        public string CCV { get; set; }


        [StringLength(100)]
        [Required]
        public string Cardholder { get; set; }

        public ApplicationUser User { get; set; }

        [StringLength(100)]
        [Required]
        public string UserId { get; set; }


    }

    public enum CreditCardType
    {
        AmericanExpress,
        Master,
        Visa
    }
}