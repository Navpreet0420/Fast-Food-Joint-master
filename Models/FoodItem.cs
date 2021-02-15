using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodJoint.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public int CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }

        public IList<Customer> Customers { get; } = new List<Customer>();
    }
}
