using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ak.ElVegetarianoFurio.Models
{
    public class Dish
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        [AllowHtml]
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
