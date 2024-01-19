namespace IvanFazlicRIN_42_22.Modals 
{
    public class Komentar
    {
        public int Id { get; set; }
        public int ArtikalId { get; set; }
        public string Tekst { get; set; }
        public Artikal Artikal { get; set; }
    }
    public class KomentarDto
    { 
        public int ArtikalId { get; set; }
        public string Tekst { get; set; }
    }
}


