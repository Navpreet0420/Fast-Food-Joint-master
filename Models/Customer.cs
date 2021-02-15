using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodJoint.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact_Number { get; set; }
        [Required]
        public int CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }
        [Required]
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
