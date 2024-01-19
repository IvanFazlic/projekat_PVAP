namespace IvanFazlicRIN_42_22.Modals
{
    public class Artikal
    {
        public int Id { get; set; }
        public required string Naziv { get; set; }
        public decimal Cena { get; set; }

        public int BojaId { get; set; }
        public Boja Boja { get; set; }

        public List<Komentar> Komentari { get; set; }
        public List<ArtikalKategorija> ArtikalKategorije { get; set; }
    }
    public class ArtikalDto
    {
        public string Naziv { get; set;}
        public decimal Cena { get; set; }
        public int BojaId { get; set;}
    }
}


