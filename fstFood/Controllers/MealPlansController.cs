using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fstFood.Data;
using fstFood.Models;

namespace fstFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlansController : ControllerBase
    {
        private readonly MealPlanContext _context;

        public MealPlansController(MealPlanContext context)
        {
            _context = context;
        }

        // GET: api/MealPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealPlan>>> GetMealPlans()
        {
            return await _context.MealPlans.ToListAsync();
        }

        // GET: api/MealPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlan>> GetMealPlan(int id)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);

            if (mealPlan == null)
            {
                return NotFound();
            }

            return mealPlan;
        }

        [HttpGet("users/{userID}")]
        public ActionResult<MealPlan> GetMealPlanByUserID(int userID)
        {
            List<MealPlan> mealPlans = _context.MealPlans.Where(mp => mp.userID == userID).ToList();

            if (mealPlans == null)
            {
                return NotFound(null);
            }

            return Ok(mealPlans);
        }

        // POST: api/MealPlans
        [HttpPost]
        public async Task<ActionResult<MealPlan>> PostMealPlan(MealPlan mealPlan)
        {
            if (mealPlan != null)
            {
                _context.MealPlans.Add(mealPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        // DELETE: api/MealPlans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MealPlan>> DeleteMealPlan(int id)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            _context.MealPlans.Remove(mealPlan);
            await _context.SaveChangesAsync();

            return mealPlan;
        }

        private bool MealPlanExists(int id)
        {
            return _context.MealPlans.Any(e => e.mealPlanID == id);
        }
    }
}
