using IvanFazlicRIN_42_22.Modals;
using Microsoft.EntityFrameworkCore;
namespace IvanFazlicRIN_42_22;

public class ApplicationDbContext : DbContext
{
    public DbSet<Artikal> Artikli { get; set; }
    public DbSet<Komentar> Komentari { get; set; }
    public DbSet<Boja> Boje { get; set; }
    public DbSet<Kategorija> Kategorije { get; set; }
    public DbSet<ArtikalKategorija> ArtikalKategorije { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtikalKategorija>()
                 .HasKey(ak => new { ak.ArtikalId, ak.KategorijaId });

        modelBuilder.Entity<ArtikalKategorija>()
            .HasOne(ak => ak.Artikal)
            .WithMany(a => a.ArtikalKategorije)
            .HasForeignKey(ak => ak.ArtikalId);

        modelBuilder.Entity<ArtikalKategorija>()
            .HasOne(ak => ak.Kategorija)
            .WithMany(k => k.ArtikalKategorije)
            .HasForeignKey(ak => ak.KategorijaId);

        modelBuilder.Entity<Boja>()
            .HasMany(b => b.Artikli)
            .WithOne(a => a.Boja)
            .HasForeignKey(a => a.BojaId);

        modelBuilder.Entity<Komentar>()
            .HasOne(k => k.Artikal)
            .WithMany(a => a.Komentari)
            .HasForeignKey(k => k.ArtikalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=SOURCE_CODE;Initial Catalog=Zadatak;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
    }
}
