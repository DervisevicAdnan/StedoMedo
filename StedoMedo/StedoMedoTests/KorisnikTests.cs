using Microsoft.VisualStudio.TestTools.UnitTesting;
using StedoMedo.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StedoMedoTests
{
    [TestClass]
    public class KorisnikTests
    {
        private List<Korisnik> _korisnici;

        [TestInitialize]
        public void Postavi()
        {
            _korisnici = new List<Korisnik>();
        }

        [TestMethod]
        public void KreirajKorisnika_UspjesnoDodajeNovogKorisnika()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            // Act
            _korisnici.Add(korisnik);

            // Assert
            Assert.AreEqual(1, _korisnici.Count);
            Assert.AreEqual("noviUser", _korisnici.First().Username);
        }

        [TestMethod]
        public void ValidacijaKorisnika_InvalidnaPolja_VracaGreske()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 0,  // Invalid id
                username: "",  // Invalid username (required)
                ime: "",  // Invalid ime (required)
                prezime: "",  // Invalid prezime (required)
                telefon: "12345",  // Invalid telefon
                email: "invalid-email",  // Invalid email
                sifraHash: ""  // Empty password
            );

            // Act
            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(6, results.Count);  // Expecting 6 validation errors
        }

        [TestMethod]
        public void ValidacijaKorisnika_ValidnaPolja_VracaBezGresaka()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            // Act
            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);  // No validation errors
        }

        [TestMethod]
        public void TelefonFormat_InvalidanTelefon_VracaGresku()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "12345",  // Invalid phone format
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            // Act
            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);  // Expecting a phone format error
            Assert.AreEqual("Telefon mora biti u formatu +387xxxxxxxx ili +387xxxxxxxxx.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void EmailFormat_InvalidanEmail_VracaGresku()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "invalid-email",  // Invalid email
                sifraHash: "hashedpassword"
            );

            // Act
            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);  // Expecting an email format error
            Assert.AreEqual("Email nije validan.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UsernameLength_InvalidUsernameLength_VracaGresku()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "a",  // Too short username
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            // Act
            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);  // Expecting a username length error
            Assert.AreEqual("Username mora imati između 3 i 50 karaktera.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void KreirajKorisnika_VecPostojiUsername_VracaFalse()
        {
            // Arrange
            var korisnik1 = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );
            _korisnici.Add(korisnik1);

            var korisnik2 = new Korisnik(
                id: 2,
                username: "noviUser",
                ime: "Ivan",
                prezime: "Horvat",
                telefon: "+38761234567",
                email: "ivan.horvat@example.com",
                sifraHash: "hashedpassword"
            );

            // Act
            bool postoji = _korisnici.Any(k => k.Username == korisnik2.Username);

            // Assert
            Assert.IsTrue(postoji);
            Assert.AreEqual(1, _korisnici.Count);  // Nema duplikata
        }

        [TestMethod]
        public void PrijaviKorisnika_IspravanUsernameISifra_VracaKorisnika()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "user1",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );
            _korisnici.Add(korisnik);

            // Act
            Korisnik prijavljenKorisnik = _korisnici.FirstOrDefault(k => k.Username == "user1" && k.SifraHash == "hashedpassword");

            // Assert
            Assert.IsNotNull(prijavljenKorisnik);
            Assert.AreEqual("user1", prijavljenKorisnik.Username);
        }

        [TestMethod]
        public void PrijaviKorisnika_NeispravanUsernameISifra_VracaNull()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "user1",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );
            _korisnici.Add(korisnik);

            // Act
            Korisnik prijavljenKorisnik = _korisnici.FirstOrDefault(k => k.Username == "user1" && k.SifraHash == "pogresnaSifra");

            // Assert
            Assert.IsNull(prijavljenKorisnik);
        }

        [TestMethod]
        public void ObrisiKorisnika_UspjesnoBriseKorisnikaIVezanePodatke()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "user1",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );
            _korisnici.Add(korisnik);

            // Act
            bool obrisan = _korisnici.Remove(korisnik);

            // Assert
            Assert.IsTrue(obrisan);
            Assert.AreEqual(0, _korisnici.Count);
        }

        [TestMethod]
        public void ObrisiKorisnika_NePostojiKorisnik_VracaFalse()
        {
            // Arrange
            var korisnik = new Korisnik(
                id: 1,
                username: "user1",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            // Act
            bool obrisan = _korisnici.Remove(korisnik);

            // Assert
            Assert.IsFalse(obrisan);
            Assert.AreEqual(0, _korisnici.Count);
        }
    }
}
