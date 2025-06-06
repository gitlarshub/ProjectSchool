using NUnit.Framework;
using ProjectSchool.Models;
using System;
using System.Collections.Generic;

namespace TestProjectSchool
{
    public class SchuleTest
    {
        private Schule schule;

        [SetUp]
        public void Setup()
        {
            schule = new Schule();
        }

        [Test]
        public void Schule_AddSchueler_IncreasesCount()
        {
            var schueler = new Schueler("10A", new DateTime(2005, 5, 15), "weiblich");

            schule.AddSchuelerToSchule(schueler);

            Assert.That(schule.AnzahlSchueler, Is.EqualTo(1));
        }

        [Test]
        public void Schule_DurchschnittsalterSchueler_CalculatesCorrectly()
        {
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10B", DateTime.Today.AddYears(-16), "weiblich"));

            var avgAge = schule.DurchschnittsalterSchueler();

            Assert.That(avgAge, Is.EqualTo(15.5f).Within(0.01f));
        }

        [Test]
        public void Schule_BerechneFrauenanteilInProzent_EmptyClass_ReturnsZero()
        {
            var schuelerList = new List<Schueler>();

            var frauenAnteil = schule.BerechneFrauenanteilInProzent(schuelerList, "10A");

            Assert.That(frauenAnteil, Is.EqualTo(0));
        }

        [Test]
        public void Schule_BerechneFrauenanteilInProzent_CalculatesCorrectly()
        {
            var schuelerList = new List<Schueler>
            {
                new Schueler("10A", DateTime.Today.AddYears(-15), "weiblich"),
                new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"),
                new Schueler("10B", DateTime.Today.AddYears(-15), "weiblich")
            };

            var frauenAnteil = schule.BerechneFrauenanteilInProzent(schuelerList, "10A");

            Assert.That(frauenAnteil, Is.EqualTo(50.0).Within(0.01));
        }

        [Test]
        public void Schule_KannKlasseUnterrichten_SufficientSeats_ReturnsTrue()
        {
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "weiblich"));
            schule.AddKlassenraumToSchule(new Klassenraum(50f, 5, false));

            var canTeach = schule.KannKlasseUnterrichten("10A", "50");

            Assert.IsTrue(canTeach);
        }

        [Test]
        public void Schule_KannKlasseUnterrichten_InsufficientSeats_ReturnsFalse()
        {
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "weiblich"));
            schule.AddKlassenraumToSchule(new Klassenraum(50f, 1, false));

            var canTeach = schule.KannKlasseUnterrichten("10A", "50");

            Assert.That(canTeach, Is.False);
        }

        [Test]
        public void Schule_KannKlasseUnterrichten_RoomNotFound_ReturnsFalse()
        {
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));

            var canTeach = schule.KannKlasseUnterrichten("10A", "NichtExistierend");

            Assert.That(canTeach, Is.False);
        }

        [Test]
        public void Schule_AnzahlSchuelerGeschlecht_ReturnsCorrectCount()
        {
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10B", DateTime.Today.AddYears(-15), "weiblich"));

            var geschlechtCount = schule.AnzahlSchuelerGeschlecht;

            Assert.That(geschlechtCount, Is.EqualTo("männliche: 1 / weibliche: 1"));
        }

        [Test]
        public void Schule_AnzahlRauemeCynap_ReturnsCorrectRooms()
        {
            schule.AddKlassenraumToSchule(new Klassenraum(50f, 30, true));
            schule.AddKlassenraumToSchule(new Klassenraum(60f, 25, false));

            var cynapRooms = schule.AnzahlRauemeCynap();

            Assert.That(cynapRooms.Count, Is.EqualTo(1));
            Assert.That(cynapRooms[0].HasCynap, Is.True);
        }
    }
}