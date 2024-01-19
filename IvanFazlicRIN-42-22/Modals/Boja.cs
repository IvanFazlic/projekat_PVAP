namespace IvanFazlicRIN_42_22.Modals
{
    public class Boja
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public List<Artikal> Artikli { get; set; }
    }
    public class BojaDto
    {
        public string Naziv { get; set; }
    }
}

