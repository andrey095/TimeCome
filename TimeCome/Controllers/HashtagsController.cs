using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeCome.Data;
using TimeCome.Models;

namespace TimeCome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HashtagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HashtagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hashtags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hashtag>>> GetHashtags()
        {
            return await _context.Hashtags.Take(10).ToListAsync();
        }

        // GET: api/Hashtags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hashtag>> GetHashtag(int id)
        {
            var hashtag = await _context.Hashtags.FindAsync(id);

            if (hashtag == null)
            {
                return NotFound();
            }

            return hashtag;
        }

        // PUT: api/Hashtags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHashtag(int id, Hashtag hashtag)
        {
            if (id != hashtag.Id)
            {
                return BadRequest();
            }

            _context.Entry(hashtag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HashtagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hashtags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hashtag>> PostHashtag(Hashtag hashtag)
        {
            _context.Hashtags.Add(hashtag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHashtag", new { id = hashtag.Id }, hashtag);
        }

        // DELETE: api/Hashtags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHashtag(int id)
        {
            var hashtag = await _context.Hashtags.FindAsync(id);
            if (hashtag == null)
            {
                return NotFound();
            }

            _context.Hashtags.Remove(hashtag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HashtagExists(int id)
        {
            return _context.Hashtags.Any(e => e.Id == id);
        }
    }
}
