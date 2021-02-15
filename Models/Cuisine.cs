using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodJoint.Models
{
    public class Cuisine
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<FoodItem> FoodItems { get; } = new List<FoodItem>();
    }
}
