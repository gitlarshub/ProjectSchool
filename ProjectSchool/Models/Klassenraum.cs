namespace ProjectSchool.Models
{
    public class Klassenraum
    {
        public int Id { get; set; }
        public int SchuleId { get; set; } 
        public float RaumInQm { get; set; }
        public int Plaetze { get; set; }
        public bool HasCynap { get; set; }

        public List<Schueler> SchuelerImRaum = new List<Schueler>();
        public Klassenraum(float raumInQm, int plaetze, bool hasCynap)
        {
            RaumInQm = raumInQm;
            Plaetze = plaetze;
            HasCynap = hasCynap;
        }
    }
}