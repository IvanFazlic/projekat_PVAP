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
    public class ArtikalKategorijasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtikalKategorijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ArtikalKategorijas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtikalKategorija>>> GetArtikalKategorije()
        {
            return await _context.ArtikalKategorije.ToListAsync();
        }

        // GET: api/ArtikalKategorijas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtikalKategorija>> GetArtikalKategorija(int id)
        {
            var artikalKategorija = await _context.ArtikalKategorije.FindAsync(id);

            if (artikalKategorija == null)
            {
                return NotFound();
            }

            return artikalKategorija;
        }

        // PUT: api/ArtikalKategorijas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikalKategorija(int id, ArtikalKategorija artikalKategorija)
        {
            if (id != artikalKategorija.ArtikalId)
            {
                return BadRequest();
            }

            _context.Entry(artikalKategorija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikalKategorijaExists(id))
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

        // POST: api/ArtikalKategorijas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtikalKategorija>> PostArtikalKategorija(ArtikalKategorijaDto artikalKategorija)
        {
            ArtikalKategorija artijalZaVracanja = new ArtikalKategorija {
                ArtikalId = artikalKategorija.ArtikalId,
                KategorijaId = artikalKategorija.KategorijaId
            };

            _context.ArtikalKategorije.Add(artijalZaVracanja);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtikalKategorijaExists(artijalZaVracanja.ArtikalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtikalKategorija", new { id = artikalKategorija.ArtikalId }, artikalKategorija);
        }

        // DELETE: api/ArtikalKategorijas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtikalKategorija(int id)
        {
            var artikalKategorija = await _context.ArtikalKategorije.FindAsync(id);
            if (artikalKategorija == null)
            {
                return NotFound();
            }

            _context.ArtikalKategorije.Remove(artikalKategorija);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtikalKategorijaExists(int id)
        {
            return _context.ArtikalKategorije.Any(e => e.ArtikalId == id);
        }
    }
}
