using NUnit.Framework;
using ProjectSchool.Models;
using System;

namespace TestProjectSchool
{
    public class SchuelerTest
    {
        private Schueler schueler;

        [SetUp]
        public void Setup()
        {
            schueler = new Schueler("10A", new DateTime(2005, 5, 15), "weiblich");
        }

        [Test]
        public void Schueler_Alter_CalculatesCorrectly()
        {
            var birthDate = DateTime.Today.AddYears(-15);

            var schueler = new Schueler("10A", birthDate, "männlich");

            Assert.That(schueler.Alter, Is.EqualTo(15));
        }

        [Test]
        public void Schueler_AddKlasse_DoesNotAddDuplicate()
        {
            schueler.AddKlasse("10A");

            schueler.AddKlasse("10A");

            Assert.That(schueler.klassen.Count, Is.EqualTo(1));
        }
    }
}