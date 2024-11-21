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
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            _korisnici.Add(korisnik);

            Assert.AreEqual(1, _korisnici.Count);
            Assert.AreEqual("noviUser", _korisnici.First().Username);
        }

        [TestMethod]
        public void ValidacijaKorisnika_InvalidnaPolja_VracaGreske()
        {
            var korisnik = new Korisnik(
                id: 0,  
                username: "", 
                ime: "", 
                prezime: "",  
                telefon: "12345",
                email: "invalid-email",  
                sifraHash: "" 
            );

            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(6, results.Count); 
        }

        [TestMethod]
        public void ValidacijaKorisnika_ValidnaPolja_VracaBezGresaka()
        {
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count); 
        }

        [TestMethod]
        public void TelefonFormat_InvalidanTelefon_VracaGresku()
        {
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "12345",  
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);  
            Assert.AreEqual("Telefon mora biti u formatu +387xxxxxxxx ili +387xxxxxxxxx.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void EmailFormat_InvalidanEmail_VracaGresku()
        {
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "invalid-email",  
                sifraHash: "hashedpassword"
            );

            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("Email nije validan.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UsernameLength_InvalidUsernameLength_VracaGresku()
        {
            var korisnik = new Korisnik(
                id: 1,
                username: "a",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            var results = new List<ValidationResult>();
            var context = new ValidationContext(korisnik);
            bool isValid = Validator.TryValidateObject(korisnik, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);  
            Assert.AreEqual("Username mora imati između 3 i 50 karaktera.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void KreirajKorisnika_VecPostojiUsername_VracaFalse()
        {
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

            bool postoji = _korisnici.Any(k => k.Username == korisnik2.Username);

            Assert.IsTrue(postoji);
            Assert.AreEqual(1, _korisnici.Count); 
        }

        [TestMethod]
        public void PrijaviKorisnika_IspravanUsernameISifra_VracaKorisnika()
        {
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

            Korisnik prijavljenKorisnik = _korisnici.FirstOrDefault(k => k.Username == "user1" && k.SifraHash == "hashedpassword");

            Assert.IsNotNull(prijavljenKorisnik);
            Assert.AreEqual("user1", prijavljenKorisnik.Username);
        }

        [TestMethod]
        public void PrijaviKorisnika_NeispravanUsernameISifra_VracaNull()
        {
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

            Korisnik prijavljenKorisnik = _korisnici.FirstOrDefault(k => k.Username == "user1" && k.SifraHash == "pogresnaSifra");

            Assert.IsNull(prijavljenKorisnik);
        }

        [TestMethod]
        public void ObrisiKorisnika_UspjesnoBriseKorisnikaIVezanePodatke()
        {
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

            bool obrisan = _korisnici.Remove(korisnik);

            Assert.IsTrue(obrisan);
            Assert.AreEqual(0, _korisnici.Count);
        }

        [TestMethod]
        public void ObrisiKorisnika_NePostojiKorisnik_VracaFalse()
        {
            var korisnik = new Korisnik(
                id: 1,
                username: "user1",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            bool obrisan = _korisnici.Remove(korisnik);

            Assert.IsFalse(obrisan);
            Assert.AreEqual(0, _korisnici.Count);
        }

        [TestMethod]
        public void ToString_ValidanKorisnik_VracaIspravanString()
        {
            var korisnik = new Korisnik(
                id: 1,
                username: "noviUser",
                ime: "Marko",
                prezime: "Perić",
                telefon: "+38761234567",
                email: "marko.peric@example.com",
                sifraHash: "hashedpassword"
            );

            var expected = "1 noviUser Marko Perić +38761234567 marko.peric@example.com";

            var result = korisnik.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToString_PraznaPolja_VracaIspravanString()
        {
            var korisnik = new Korisnik(
                id: 2,
                username: "",
                ime: "",
                prezime: "",
                telefon: "+38761234567",
                email: "",
                sifraHash: "hashedpassword"
            );

            var expected = "2    +38761234567 ";

            var result = korisnik.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToString_NullVrijednosti_VracaIspravanString()
        {
            var korisnik = new Korisnik(
                id: 3,
                username: null,
                ime: null,
                prezime: null,
                telefon: "+38761234567",
                email: null,
                sifraHash: "hashedpassword"
            );

            var expected = "3    +38761234567 ";

            var result = korisnik.ToString();

            Assert.AreEqual(expected, result);
        }



    }
}
