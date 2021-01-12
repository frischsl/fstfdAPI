using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fstFood.Models;
using Microsoft.EntityFrameworkCore;

namespace fstFood.Data
{
    public class MealPlanContext : DbContext
    {
        public MealPlanContext(DbContextOptions<MealPlanContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<MealPlan> MealPlans{ get; set; }
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Friend> Friends{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<MealPlan>().ToTable("MealPlans");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Friend>().ToTable("Friends");
        }
    }
}
