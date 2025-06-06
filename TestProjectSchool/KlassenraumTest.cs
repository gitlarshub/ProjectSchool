using NUnit.Framework;
using ProjectSchool.Models;
using System;

namespace TestProjectSchool
{
    public class KlassenraumTests
    {
        private Klassenraum klassenraum;
        private Schueler schueler;

        [SetUp]
        public void Setup()
        {
            klassenraum = new Klassenraum(50.0f, 30, true);
            schueler = new Schueler("10A", new DateTime(2005, 5, 15), "weiblich");
        }

        [Test]
        public void Klassenraum_AddSchueler_IncreasesCount()
        {
            klassenraum.SchuelerImRaum.Add(schueler);

            Assert.That(klassenraum.SchuelerImRaum.Count, Is.EqualTo(1));
        }
    }
}