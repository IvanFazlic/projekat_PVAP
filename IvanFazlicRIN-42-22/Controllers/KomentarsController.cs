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
    public class KomentarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KomentarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Komentars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Komentar>>> GetKomentari()
        {
            return await _context.Komentari.Include(x => x.Artikal).ToListAsync();
        }

        // GET: api/Komentars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Komentar>> GetKomentar(int id)
        {
            var komentar = await _context.Komentari.FindAsync(id);

            if (komentar == null)
            {
                return NotFound();
            }

            return komentar;
        }

        // PUT: api/Komentars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKomentar(int id, Komentar komentar)
        {
            if (id != komentar.Id)
            {
                return BadRequest();
            }

            _context.Entry(komentar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KomentarExists(id))
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

        // POST: api/Komentars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Komentar>> PostKomentar(KomentarDto komentar)
        {
            if (komentar == null)
            {
                return BadRequest();
            }
            var noviKomentar = new Komentar
            {
                ArtikalId = komentar.ArtikalId,
                Tekst = komentar.Tekst,
            };
            _context.Komentari.Add(noviKomentar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKomentar", new { id = noviKomentar.Id }, komentar);
        }

        // DELETE: api/Komentars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKomentar(int id)
        {
            var komentar = await _context.Komentari.FindAsync(id);
            if (komentar == null)
            {
                return NotFound();
            }

            _context.Komentari.Remove(komentar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KomentarExists(int id)
        {
            return _context.Komentari.Any(e => e.Id == id);
        }
    }
}
