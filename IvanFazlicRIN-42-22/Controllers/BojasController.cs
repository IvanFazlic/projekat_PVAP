using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IvanFazlicRIN_42_22;
using IvanFazlicRIN_42_22.Modals;

namespace IvanFazlicRIN_42_22.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BojasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BojasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bojas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boja>>> GetBoje()
        {
            return await _context.Boje.ToListAsync();
        }

        // GET: api/Bojas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Boja>> GetBoja(int id)
        {
            var boja = await _context.Boje.FindAsync(id);

            if (boja == null)
            {
                return NotFound();
            }

            return boja;
        }

        // PUT: api/Bojas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoja(int id, Boja boja)
        {
            if (id != boja.Id)
            {
                return BadRequest();
            }

            _context.Entry(boja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BojaExists(id))
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

        // POST: api/Bojas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Boja>> PostBoja(BojaDto boja)
        {
            
            if (boja == null)
            {
                return NoContent();
            }
            var brojUBazi = _context.Boje.FirstOrDefault(x => x.Naziv == boja.Naziv);
            if (brojUBazi != null)
            {
                return BadRequest();
            }
            var bojaZaVracanje = new Boja
            {
                Naziv = boja.Naziv,
            };
            _context.Boje.Add(bojaZaVracanje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoja", new { id = bojaZaVracanje.Id }, boja);
        }

        // DELETE: api/Bojas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoja(int id)
        {
            var boja = await _context.Boje.FindAsync(id);
            if (boja == null)
            {
                return NotFound();
            }

            _context.Boje.Remove(boja);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BojaExists(int id)
        {
            return _context.Boje.Any(e => e.Id == id);
        }
    }
}
