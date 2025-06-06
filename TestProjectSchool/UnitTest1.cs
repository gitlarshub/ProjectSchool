using NUnit.Framework;
using ProjectSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectSchool
{
    public class Tests
    {
        private Schule schule;
        private Schueler schueler;
        private Klassenraum klassenraum;

        [SetUp]
        public void Setup()
        {
            schule = new Schule();
            schueler = new Schueler("10A", new DateTime(2005, 5, 15), "weiblich");
            klassenraum = new Klassenraum(50.0f, 30, true);
        }

        [Test]
        public void Person_Geschlecht_SetValidValue_SetsCorrectly()
        {
            // Arrange
            var person = new Person(new DateTime(2000, 1, 1), "männlich");

            // Act
            person.Geschlecht = "weiblich";

            // Assert
            Assert.That(person.Geschlecht, Is.EqualTo("weiblich"));
        }

        [Test]
        public void Person_Geschlecht_SetInvalidValue_SetsUnbekannt()
        {
            // Arrange
            var person = new Person(new DateTime(2000, 1, 1), "m");

            // Act
            person.Geschlecht = "invalid";

            // Assert
            Assert.That(person.Geschlecht, Is.EqualTo("unbekannt"));
        }

        [Test]
        public void Schueler_Alter_CalculatesCorrectly()
        {
            // Arrange
            var birthDate = DateTime.Today.AddYears(-15);

            // Act
            var schueler = new Schueler("10A", birthDate, "männlich");

            // Assert
            Assert.That(schueler.Alter, Is.EqualTo(15));
        }

        [Test]
        public void Schueler_AddKlasse_DoesNotAddDuplicate()
        {
            // Arrange
            schueler.AddKlasse("10A");

            // Act
            schueler.AddKlasse("10A");

            // Assert
            Assert.That(schueler.klassen.Count, Is.EqualTo(1));
        }


        [Test]
        public void Schule_AddSchueler_IncreasesCount()
        {
            // Act
            schule.AddSchuelerToSchule(schueler);

            // Assert
            Assert.That(schule.AnzahlSchueler, Is.EqualTo(1));
        }

        [Test]
        public void Schule_DurchschnittsalterSchueler_CalculatesCorrectly()
        {
            // Arrange
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10B", DateTime.Today.AddYears(-16), "weiblich"));

            // Act
            var avgAge = schule.DurchschnittsalterSchueler();

            // Assert
            Assert.That(avgAge, Is.EqualTo(15.5f).Within(0.01f));
        }

        [Test]
        public void Schule_BerechneFrauenanteilInProzent_EmptyClass_ReturnsZero()
        {
            // Arrange
            var schuelerList = new List<Schueler>();

            // Act
            var frauenAnteil = schule.BerechneFrauenanteilInProzent(schuelerList, "10A");

            // Assert
            Assert.That(frauenAnteil, Is.EqualTo(0));
        }

        [Test]
        public void Schule_BerechneFrauenanteilInProzent_CalculatesCorrectly()
        {
            // Arrange
            var schuelerList = new List<Schueler>
            {
                new Schueler("10A", DateTime.Today.AddYears(-15), "weiblich"),
                new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"),
                new Schueler("10B", DateTime.Today.AddYears(-15), "weiblich")
            };

            // Act
            var frauenAnteil = schule.BerechneFrauenanteilInProzent(schuelerList, "10A");

            // Assert
            Assert.That(frauenAnteil, Is.EqualTo(50.0).Within(0.01));
        }

        [Test]
        public void Schule_KannKlasseUnterrichten_SufficientSeats_ReturnsTrue()
        {
            // Arrange
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "weiblich"));
            schule.AddKlassenraumToSchule(new Klassenraum(50f, 5, false));

            var canTeach = schule.KannKlasseUnterrichten("10A", "50");

            // Assert
            Assert.IsTrue(canTeach);
        }

        [Test]
        public void Schule_KannKlasseUnterrichten_InsufficientSeats_ReturnsFalse()
        {
            // Arrange
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "weiblich"));
            schule.AddKlassenraumToSchule(new Klassenraum(50f, 1, false));

            // Act
            var canTeach = schule.KannKlasseUnterrichten("10A", "Raum1");

            // Assert
            Assert.That(canTeach, Is.False);
        }

        [Test]
        public void Schule_KannKlasseUnterrichten_RoomNotFound_ReturnsFalse()
        {
            // Arrange
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));

            // Act
            var canTeach = schule.KannKlasseUnterrichten("10A", "NichtExistierend");

            // Assert
            Assert.That(canTeach, Is.False);
        }

        [Test]
        public void Schule_AnzahlSchuelerGeschlecht_ReturnsCorrectCount()
        {
            // Arrange
            schule.AddSchuelerToSchule(new Schueler("10A", DateTime.Today.AddYears(-15), "männlich"));
            schule.AddSchuelerToSchule(new Schueler("10B", DateTime.Today.AddYears(-15), "weiblich"));

            // Act
            var geschlechtCount = schule.AnzahlSchuelerGeschlecht;

            // Assert
            Assert.That(geschlechtCount, Is.EqualTo("männliche: 1 / weibliche: 1"));
        }

        [Test]
        public void Klassenraum_AddSchueler_IncreasesCount()
        {
            // Act
            klassenraum.SchuelerImRaum.Add(schueler);

            // Assert
            Assert.That(klassenraum.SchuelerImRaum.Count, Is.EqualTo(1));
        }

        [Test]
        public void Schule_AnzahlRauemeCynap_ReturnsCorrectRooms()
        {
            // Arrange
            schule.AddKlassenraumToSchule(new Klassenraum(50f, 30, true));
            schule.AddKlassenraumToSchule(new Klassenraum(60f, 25, false));
            // Act
            var cynapRooms = schule.AnzahlRauemeCynap();

            // Assert
            Assert.That(cynapRooms.Count, Is.EqualTo(1));
            Assert.That(cynapRooms[0].HasCynap, Is.True);
        }
    }
}