namespace ProjectSchool.Models
{
    public class Schule
    {
        public int Id { get; set; }
        public List<Schueler> SchuelerList = new List<Schueler>();
        public List<Klassenraum> KlassenraumList = new List<Klassenraum>();

        public void AddSchuelerToSchule(Schueler? schueler)
        {
            if (schueler == null)
            {
                throw new InvalidDataException();
            }
            SchuelerList.Add(schueler);
        }
        public void AddKlassenraumToSchule(Klassenraum klassenraum)
        {
            KlassenraumList.Add(klassenraum);
        }
        public int AnzahlSchueler
        {
            get { return SchuelerList.Count; }
        }
        public int AnzahlKlassenRaum
        {
            get { return KlassenraumList.Count; }
        }
        public List<Klassenraum> AnzahlRauemeCynap()
        {
            List<Klassenraum> KlassenraumCynap = new List<Klassenraum>();
            foreach (Klassenraum klassenraum in KlassenraumList)
            {
                if (klassenraum.HasCynap)
                {
                    KlassenraumCynap.Add(klassenraum);
                }
            }
            return KlassenraumCynap;
        }

        public bool KannKlasseUnterrichten(string klasse, string raumName)
        {
            int schuelerInKlasse = 0;
            Klassenraum raum = null;

            foreach (Schueler schueler in SchuelerList)
            {
                if (schueler.Klasse == klasse)
                {
                    schuelerInKlasse++;
                }
            }

            foreach (Klassenraum klassenraum in KlassenraumList)
            {
                if (klassenraum.RaumInQm.ToString() == raumName)
                {
                    raum = klassenraum;
                    break;
                }
            }
            if (raum == null) return false;

            return raum.Plaetze >= schuelerInKlasse;
        }

    }
}