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
    public class PostsController : ControllerBase
    {
        private readonly MealPlanContext _context;

        public PostsController(MealPlanContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{userID}")]
        public ActionResult<IEnumerable<Post>> GetPostByUserID(int userID)
        {
            var posts = _context.Posts.Where(p => p.userID == userID)?.ToList();

            if (posts == null)
            {
                return NotFound();
            }

            return posts;
        }




        // GET: api/Posts/5
        [HttpGet("bulk/{userIDs}")]
        public ActionResult<IEnumerable<Post>> GetPostBulk(string userIDs)
        {
            List<int> ids = new List<int>();
            userIDs.Split(',')?.ToList().ForEach(id => ids.Add(Convert.ToInt32(id)));

            List<Post> posts = new List<Post>();

            if (ids.Count > 0)
            {
                ids.ForEach(id => {
                    var postsForId = _context.Posts.Where(p => p.userID == id)?.ToList();
                    posts.AddRange(postsForId);
                });

                return Ok(posts);
            }
            //var posts = _context.Posts.Where(p => p.userID == userID)?.ToList();

            //if (posts == null)
            //{
            //    return NotFound();
            //}

            return BadRequest(new Post());
        }



        // POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            if (post != null)
            {
                post.postedAt = DateTime.Now;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.postID == id);
        }
    }
}
