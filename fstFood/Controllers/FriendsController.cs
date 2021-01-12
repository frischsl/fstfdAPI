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
    public class FriendsController : ControllerBase
    {
        private readonly MealPlanContext _context;

        public FriendsController(MealPlanContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            return await _context.Friends.ToListAsync();
        }

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Friend>>> GetFriend(int id)
        {
            var friends = _context.Friends.Where(f => f.userIDMain == id)?.ToList();

            if (friends == null)
            {
                return NotFound();
            }

            return friends;
        }


        // POST: api/Friends
        [HttpPost]
        public async Task<ActionResult<Friend>> PostFriend(Friend friend)
        {
            if (friend != null)
            {
                _context.Friends.Add(friend);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
            
        }

        // POST: api/Friends/delete
        [Route("delete")]
        [HttpPost]
        public async Task<ActionResult<Friend>> PostDeleteFriend(Friend friend)
        {
            if (friend != null)
            {
                Friend delFriend =_context.Friends.Where(f => f.userIDMain == friend.userIDMain && f.userIDFriend == friend.userIDFriend)?.SingleOrDefault();
                _context.Friends.Remove(delFriend);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();

        }

        // POST: api/Friends/isFriend
        [Route("isFriend")]
        [HttpPost]
        public async Task<ActionResult<Friend>> PostIsFriend(Friend friend)
        {
            if (friend != null)
            {
                bool friendExists = _context.Friends.Where(f => f.userIDMain == friend.userIDMain && f.userIDFriend == friend.userIDFriend)?.SingleOrDefault() != null;

                if (friendExists)
                    return Ok();
                else
                    return BadRequest();
            }

            return BadRequest();

        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> DeleteFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();

            return friend;
        }

        private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.id == id);
        }
    }
}
