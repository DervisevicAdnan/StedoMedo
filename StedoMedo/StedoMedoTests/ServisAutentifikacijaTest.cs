using Microsoft.VisualStudio.TestTools.UnitTesting;
using StedoMedo.Models;
using StedoMedo.Data;
using StedoMedo.Services.ServisAutentifikacija;
using System.Collections.Generic;
using System.Linq;

namespace StedoMedoTests
{
    [TestClass]
    public class ServisAutentifikacijaTest
    {
        private DbClass _db;
        private ServisAutentifikacija _servis;

        [TestInitialize]
        public void Postavi()
        {
            _db = new DbClass
            {
                Korisnici = new List<Korisnik>()
            };
            _servis = new ServisAutentifikacija(_db);
        }

        [TestMethod]
        public void KreirajKorisnika_UspjesnoDodajeNovogKorisnika()
        {
            bool rezultat = _servis.KreirajKorisnika("noviUser", "Ime", "Prezime", "123456", "email@gmail.com", "password");

            Assert.IsTrue(rezultat);
            Assert.AreEqual(1, _db.Korisnici.Count);
            Assert.AreEqual("noviUser", _db.Korisnici.First().Username);
        }

        [TestMethod]
        public void KreirajKorisnika_VecPostojiUsername_VracaFalse()
        {
            _servis.KreirajKorisnika("noviUser", "Ime", "Prezime", "123456", "email@gmail.com", "password");
            bool rezultat = _servis.KreirajKorisnika("noviUser", "Ime2", "Prezime2", "654321", "email2@gmail.com", "password2");

            Assert.IsFalse(rezultat);
            Assert.AreEqual(1, _db.Korisnici.Count);
        }

        [TestMethod]
        public void PrijaviKorisnika_IspravanUsernameISifra_VracaKorisnika()
        {
            _servis.KreirajKorisnika("user1", "Ime", "Prezime", "123456", "email@gmail.com", "password");
            Korisnik korisnik = _servis.PrijaviKorisnika("user1", "password");

            Assert.IsNotNull(korisnik);
            Assert.AreEqual("user1", korisnik.Username);
        }

        [TestMethod]
        public void PrijaviKorisnika_NeispravanUsernameISifra_VracaNull()
        {
            _servis.KreirajKorisnika("user1", "Ime", "Prezime", "123456", "email@gmail.com", "password");
            Korisnik korisnik = _servis.PrijaviKorisnika("user1", "pogresnaSifra");

            Assert.IsNull(korisnik);
        }

        [TestMethod]
        public void OdjaviKorisnika_PostojeciKorisnik_VracaTrue()
        {
            _servis.KreirajKorisnika("user1", "Ime", "Prezime", "123456", "email@gmail.com", "password");
            Korisnik korisnik = _db.Korisnici.First();
            bool rezultat = _servis.OdjaviKorisnika(korisnik);

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void OdjaviKorisnika_NePostojiKorisnik_VracaFalse()
        {
            Korisnik korisnik = new Korisnik(1, "nePostoji", "Ime", "Prezime", "123456", "email@gmail.com", "password");
            bool rezultat = _servis.OdjaviKorisnika(korisnik);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void ObrisiKorisnika_UspjesnoBriseKorisnikaIVezanePodatke()
        {
            Korisnik korisnik = new Korisnik(1, "user1", "Ime", "Prezime", "123456", "email@gmail.com", "password");
            _db.Korisnici.Add(korisnik);

            bool rezultat = _servis.ObrisiKorisnika(korisnik);

            Assert.IsTrue(rezultat);
            Assert.IsFalse(_db.Korisnici.Any());
        }
    }
}
