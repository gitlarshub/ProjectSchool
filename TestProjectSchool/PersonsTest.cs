using NUnit.Framework;
using ProjectSchool.Models;
using System;

namespace TestProjectSchool
{
    public class PersonTests
    {
        [Test]
        public void Person_Geschlecht_SetValidValue_SetsCorrectly()
        {
            var person = new Person(new DateTime(2000, 1, 1), "männlich");

            person.Geschlecht = "weiblich";

            Assert.That(person.Geschlecht, Is.EqualTo("weiblich"));
        }

        [Test]
        public void Person_Geschlecht_SetInvalidValue_SetsUnbekannt()
        {
            var person = new Person(new DateTime(2000, 1, 1), "m");

            person.Geschlecht = "invalid";

            Assert.That(person.Geschlecht, Is.EqualTo("unbekannt"));
        }
    }
}