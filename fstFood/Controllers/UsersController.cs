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
    public class UsersController : ControllerBase
    {
        private readonly MealPlanContext _context;

        public UsersController(MealPlanContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // POST: api/Users/register
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Users>> Register(Users users)
        {
            bool isUnique = _context.Users.Where(u => u.email == users.email).ToList().Count == 0;
            if (isUnique)
            {
                if (users.guid == null)
                {
                    users.guid = Guid.NewGuid().ToString();
                }
                users.joined = DateTime.Now;
                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }


        // POST: api/Users/login
        [Route("login")]
        [HttpPost]
        public ActionResult<Users> Login(Users users)
        {
            Users user = _context.Users.Where(u => u.email == users.email && u.password == users.password).ToList().SingleOrDefault();

            if (user != null)
            {
                return Ok(user);
                //return CreatedAtAction("GetUsers", new { id = user.userID }, users);
            }
            else
                return NotFound(null);//BadRequest(new Users());
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.userID == id);
        }
    }
}
