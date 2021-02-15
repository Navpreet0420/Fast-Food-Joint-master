using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FastFoodJoint.Models;

namespace FastFoodJoint.Data
{
    public class FastFoodJointContext : DbContext
    {
        public FastFoodJointContext(DbContextOptions<FastFoodJointContext> options)
            : base(options)
        {
        }

        public DbSet<FastFoodJoint.Models.Cuisine> Cuisines { get; set; }
        public DbSet<FastFoodJoint.Models.FoodItem> FoodItems { get; set; }
        public DbSet<FastFoodJoint.Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().
                HasOne(c => c.FoodItem).
                WithMany(p => p.Customers).
                OnDelete(DeleteBehavior.Restrict);


        }
    }
}
