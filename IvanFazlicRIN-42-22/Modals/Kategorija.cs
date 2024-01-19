namespace IvanFazlicRIN_42_22.Modals
{
    public class Kategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public List<ArtikalKategorija> ArtikalKategorije { get; set; }
    }
    public class KategorijaDto
    {
        public string Naziv { get; set; }
    }
}
