using fstFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fstFood.Data
{
    public static class DbInitializer
    {

        public static void Initialize(MealPlanContext context)
        {
            context.Database.EnsureCreated();

            if (context.MealPlans.Any())
            {
                return;   // DB has been seeded
            }

            var mealplans = new MealPlan[]
            {
                new MealPlan{mealPlanID = 1, queryString = "asdf", title = "asfas", userID = 1},
                new MealPlan{mealPlanID = 2, queryString = "asdf", title = "asfas", userID = 1},
                new MealPlan{mealPlanID = 3, queryString = "asdf", title = "asfas", userID = 1},
                

            };
            foreach (MealPlan mp in mealplans)
            {
                context.Entry(mp).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.MealPlans.Add(mp);
            }
            context.SaveChanges();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new Users[]
            {
                new Users{userID = 1, email="test1@email.com", password="secret-password", guid=Guid.NewGuid().ToString() },
                new Users{userID = 2, email="test2@email.com", password="secret-password", guid=Guid.NewGuid().ToString() },
                new Users{userID = 3, email="test3@email.com", password="secret-password", guid=Guid.NewGuid().ToString() },
                new Users{userID = 4, email="test4@email.com", password="secret-password", guid=Guid.NewGuid().ToString() },
                new Users{userID = 5, email="test5@email.com", password="secret-password", guid=Guid.NewGuid().ToString() },
                new Users{userID = 6, email="test6@email.com", password="secret-password", guid=Guid.NewGuid().ToString() },
            
            };
            foreach (Users u in users)
            {
                context.Entry(u).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}