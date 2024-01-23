using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IvanFazlicRIN_42_22;
using IvanFazlicRIN_42_22.Modals;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
        [HttpGet("{akrtialId}/{kategorijaId}")]
        public async Task<ActionResult<ArtikalKategorija>> GetArtikalKategorija(int akrtialId, int kategorijaId)
        {
            var artikalKategorija = await _context.ArtikalKategorije
                .FirstOrDefaultAsync(x => x.ArtikalId == akrtialId && x.KategorijaId == kategorijaId);

            if (artikalKategorija == null)
            {
                return NotFound();
            }

            return artikalKategorija;
        }

        // POST: api/ArtikalKategorijas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtikalKategorija>> PostArtikalKategorija(ArtikalKategorijaDto artikalKategorija)
        {
            var pronadjenArtikal = await _context.Artikli.FindAsync(artikalKategorija.ArtikalId);
            var pronadjenaKategorija = await _context.Kategorije.FindAsync(artikalKategorija.KategorijaId);

            if (pronadjenArtikal == null || pronadjenaKategorija == null)
            {
                return BadRequest("Artikal ili kategorija ne postoje.");
            }

            // Check if the ArtikalKategorija already exists
            if (ArtikalKategorijaExists(artikalKategorija.ArtikalId, artikalKategorija.KategorijaId))
            {
                return Conflict("ArtikalKategorija već postoji.");
            }

            var artikalZaVracanje = new ArtikalKategorija
            {
                ArtikalId = artikalKategorija.ArtikalId,
                KategorijaId = artikalKategorija.KategorijaId
            };

            _context.ArtikalKategorije.Add(artikalZaVracanje);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Greška pri čuvanju podataka.");
            }

            return Ok();
        }


        // DELETE: api/ArtikalKategorijas/5
        [HttpDelete("{akrtialId}/{kategorijaId}")]
        public async Task<IActionResult> DeleteArtikalKategorija(int akrtialId, int kategorijaId)
        {
            var artikalKategorija = await _context.ArtikalKategorije
                .FirstOrDefaultAsync(x => x.ArtikalId == akrtialId && x.KategorijaId == kategorijaId);

            if (artikalKategorija == null)
            {
                return NotFound();
            }

            _context.ArtikalKategorije.Remove(artikalKategorija);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtikalKategorijaExists(int artikalId,int kategorijaId)
        {
            return _context.ArtikalKategorije.Any(e => e.ArtikalId == artikalId && e.KategorijaId == kategorijaId);
        }
    }
}
