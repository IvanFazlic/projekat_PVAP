namespace IvanFazlicRIN_42_22.Modals
{
    public class ArtikalKategorija
    {
        public int ArtikalId { get; set; }
        public Artikal Artikal { get; set; }

        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
    }
    public class ArtikalKategorijaDto
    {
        public int ArtikalId { get; set; }
        public int KategorijaId { get; set; }
    }
}
