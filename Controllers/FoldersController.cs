using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly BackendContext _context;

        public FoldersController(BackendContext context)
        {
            _context = context;
        }

        // GET: api/Folders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolders()
        {
            return await _context.Folders.Include(l => l.Cases).ToListAsync();
        }

        // GET: api/Folders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Folder>> GetFolder(long id)
        {
            var folder = await _context.Folders.FindAsync(id);

            if (folder == null)
            {
                return NotFound();
            }

            return folder;
        }

        // PUT: api/Folders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolder(long id, Folder folder)
        {
            if (id != folder.Id)
            {
                return BadRequest();
            }

            _context.Entry(folder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FolderExists(id))
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

        // POST: api/Folders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Folder>> PostFolder(Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolder", new { id = folder.Id }, folder);
        }

        // DELETE: api/Folders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Folder>> DeleteFolder(long id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();

            return folder;
        }

        private bool FolderExists(long id)
        {
            return _context.Folders.Any(e => e.Id == id);
        }
    }
}
