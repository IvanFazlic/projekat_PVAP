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
    public class ArtikalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtikalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Artikals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artikal>>> GetArtikli()
        {
            return await _context.Artikli
                .Include(x => x.Boja)
                .Include(x => x.Komentari)
                .Include(x => x.ArtikalKategorije)
                .ThenInclude(x => x.Kategorija)
                .ToListAsync();
        }

        // GET: api/Artikals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artikal>> GetArtikal(int id)
        {
            var artikal = await _context.Artikli
                                .Include(x => x.Boja)
                                .Include(x => x.Komentari)
                                .Include(x => x.ArtikalKategorije)
                                .ThenInclude(x=> x.Kategorija)
                                .FirstOrDefaultAsync(a => a.Id == id);

            if (artikal == null)
            {
                return NotFound();
            }

            return artikal;
        }

        // PUT: api/Artikals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikal(int id, ArtikalDto artikal)
        {
            var artikalZaVracanje = await _context.Artikli
                                .Include(x => x.Boja)
                                .Include(x => x.Komentari)
                                .Include(x => x.ArtikalKategorije)
                                .ThenInclude(x => x.Kategorija)
                                .FirstOrDefaultAsync(a => a.Id == id);
            var proveraBoje = await _context.Boje.FindAsync(artikal.BojaId);
            if (proveraBoje == null) 
            {  
                return NotFound("Bad bojaId"); 
            }
            if (artikalZaVracanje==null || id != artikalZaVracanje.Id)
            {
                return BadRequest();
            }

            artikalZaVracanje.Naziv = artikal.Naziv;
            artikalZaVracanje.Cena = artikal.Cena;
            artikalZaVracanje.BojaId = artikal.BojaId;

            _context.Entry(artikalZaVracanje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikalExists(id))
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

        // POST: api/Artikals
        [HttpPost]
        public async Task<ActionResult<Artikal>> PostArtikal(ArtikalDto artikal)
        {
            if (artikal == null)
            {
                return NoContent();
            }
            var artikalZaVracanje = new Artikal
            {
                Cena = artikal.Cena,
                Naziv = artikal.Naziv,
                BojaId = artikal.BojaId,

            };
            _context.Artikli.Add(artikalZaVracanje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtikal", new { id = artikalZaVracanje.Id }, artikalZaVracanje);
        }

        // DELETE: api/Artikals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtikal(int id)
        {
            var artikal = await _context.Artikli.FindAsync(id);
            if (artikal == null)
            {
                return NotFound();
            }

            _context.Artikli.Remove(artikal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtikalExists(int id)
        {
            return _context.Artikli.Any(e => e.Id == id);
        }
    }
}
