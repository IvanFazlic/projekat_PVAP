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
    public class KategorijasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KategorijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Kategorijas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kategorija>>> GetKategorije()
        {
            return await _context.Kategorije.ToListAsync();
        }

        // GET: api/Kategorijas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kategorija>> GetKategorija(int id)
        {
            var kategorija = await _context.Kategorije.FindAsync(id);

            if (kategorija == null)
            {
                return NotFound();
            }

            return kategorija;
        }

        // PUT: api/Kategorijas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategorija(int id, KategorijaDto kategorija)
        {
            var kategorijaZaPretragu = _context.Kategorije.FirstOrDefault(x => x.Id == id);
            if (kategorijaZaPretragu == null)
            {
                return BadRequest();
            }
            kategorijaZaPretragu.Naziv = kategorija.Naziv;
            _context.Entry(kategorijaZaPretragu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorijaExists(id))
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

        // POST: api/Kategorijas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kategorija>> PostKategorija(KategorijaDto kategorija)
        {
            var postojiKategorija = _context.Kategorije.FirstOrDefault(a => a.Naziv == kategorija.Naziv);
            if (postojiKategorija != null)
            {
                return BadRequest("Kategorija vec postoji");
            }
            if (kategorija == null)
            {
                return NoContent();
            }
            Kategorija KategorijaZaVracanje = new Kategorija
            {
                Naziv = kategorija.Naziv,
            };
            _context.Kategorije.Add(KategorijaZaVracanje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKategorija", new { id = KategorijaZaVracanje.Id }, kategorija);
        }

        // DELETE: api/Kategorijas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategorija(int id)
        {
            var kategorija = await _context.Kategorije.FindAsync(id);
            if (kategorija == null)
            {
                return NotFound();
            }

            _context.Kategorije.Remove(kategorija);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KategorijaExists(int id)
        {
            return _context.Kategorije.Any(e => e.Id == id);
        }
    }
}
