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
    public class TaskFilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskFile>>> GetTaskFiles()
        {
            return await _context.TaskFiles.ToListAsync();
        }

        // GET: api/TaskFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskFile>> GetTaskFile(int id)
        {
            var taskFile = await _context.TaskFiles.FindAsync(id);

            if (taskFile == null)
            {
                return NotFound();
            }

            return taskFile;
        }

        // PUT: api/TaskFiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskFile(int id, TaskFile taskFile)
        {
            if (id != taskFile.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskFileExists(id))
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

        // POST: api/TaskFiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskFile>> PostTaskFile(TaskFile taskFile)
        {
            _context.TaskFiles.Add(taskFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskFile", new { id = taskFile.Id }, taskFile);
        }

        // DELETE: api/TaskFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskFile(int id)
        {
            var taskFile = await _context.TaskFiles.FindAsync(id);
            if (taskFile == null)
            {
                return NotFound();
            }

            _context.TaskFiles.Remove(taskFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskFileExists(int id)
        {
            return _context.TaskFiles.Any(e => e.Id == id);
        }
    }
}
