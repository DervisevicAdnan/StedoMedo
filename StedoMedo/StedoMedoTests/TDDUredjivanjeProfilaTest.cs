using Moq;
using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.ServisAutentifikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    namespace StedoMedoTests
    {
        [TestClass]
        public class TDDUredjivanjeProfilaTest
        {
            private Mock<IDbClass> dbClassMock;
            private Korisnik testKorisnik;
            private ServisAutentifikacija servis;
            [TestInitialize]
            public void Setup()
            {

                testKorisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+387123456", "adnan_prsut@hotmail.com", "sifra123");

                dbClassMock = new Mock<IDbClass>();

                dbClassMock.Setup(db => db.Korisnici).Returns(new List<Korisnik> { testKorisnik });
                servis = new ServisAutentifikacija(dbClassMock.Object);
            }

            [TestMethod]
            public void UredjivanjeKorisnika_ValidniPodaci_Prolazi()
            {
                Korisnik noviKorisnik = new Korisnik(1, "adnan", "Adnan", "Pršut", "+387123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
                Assert.AreEqual(novi, rez);

            }
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanID_DizeException()
            {
                Korisnik noviKorisnik = new Korisnik(55, "adnan", "Adnan", "Pršut", "+387123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);

            }
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanUsername_DizeException()
            {
                Korisnik noviKorisnik = new Korisnik(1, "1adnan", "Adnan", "Pršut", "+387123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
            }
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanoIme_DizeException()
            {
                Korisnik noviKorisnik = new Korisnik(1, "adnan", "1Adnan", "Pršut", "+387123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanoPrezime_DizeException()
            {
                Korisnik noviKorisnik = new Korisnik(55, "adnan", "Adnan", "1Pršut", "+387123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
            }


        }
    }
}
    

