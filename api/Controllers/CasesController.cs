using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Entities;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly BackendContext _context;

        public CasesController(BackendContext context)
        {
            _context = context;
        }

        // GET: api/Cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LegalCase>>> GetCases()
        {
            return await _context.Cases.ToListAsync();
        }

        // GET: api/Cases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalCase>> GetLegalCase(long id)
        {
            var legalCase = await _context.Cases.FindAsync(id);

            if (legalCase == null)
            {
                return NotFound();
            }

            return legalCase;
        }

        // PUT: api/Cases/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLegalCase(long id, LegalCase legalCase)
        {
            if (id != legalCase.Id)
            {
                return BadRequest();
            }

            _context.Entry(legalCase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegalCaseExists(id))
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

        // POST: api/Cases
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LegalCase>> PostLegalCase(LegalCase legalCase)
        {
            _context.Cases.Add(legalCase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLegalCase", new { id = legalCase.Id }, legalCase);
        }

        // DELETE: api/Cases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LegalCase>> DeleteLegalCase(long id)
        {
            var legalCase = await _context.Cases.FindAsync(id);
            if (legalCase == null)
            {
                return NotFound();
            }

            _context.Cases.Remove(legalCase);
            await _context.SaveChangesAsync();

            return legalCase;
        }

        private bool LegalCaseExists(long id)
        {
            return _context.Cases.Any(e => e.Id == id);
        }
    }
}
