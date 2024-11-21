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

        [TestClass]
    public class TDDEditovanjeProfilaTest
    {
        private Mock<IDbClass> dbClassMock;
        private Korisnik testKorisnik;
        private ServisAutentifikacija servis;
        [TestInitialize]
        public void Setup()
        {

            testKorisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+38762123456", "adnan_prsut@hotmail.com", "sifra123");

            dbClassMock = new Mock<IDbClass>();

            dbClassMock.Setup(db => db.Korisnici).Returns(new List<Korisnik> { testKorisnik });
            servis=new ServisAutentifikacija(dbClassMock.Object);
        }

        [TestMethod]
        public void TestPromjeneBrojaTelefona()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+38762654321", "adnan_prsut@hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
            Assert.AreEqual(testKorisnik.Telefon, korisnik.Telefon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNevalidniBrojTelefona()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+387abcdefg", "adnan_prsut@hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLosFormatBrojTelefona()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "061123456", "adnan_prsut@hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPredugBrojTelefona()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+38712345612789", "adnan_prsut@hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPrekratakBrojTelefona()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+387123", "adnan_prsut@hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
        }

        [TestMethod]
        public void TestPromjeneEmailAdrese()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+38762654321", "adnan_prsut123@hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
            Assert.AreEqual(testKorisnik.Email,korisnik.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNevalidnaEmailAdresa()
        {
            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+387654321", "adnan_prsut123hotmail.com", "sifra123");
            servis.UredjivanjeProfila(korisnik);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmailAdreseBezDomene()

        {

            

            Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+387654321", "adnan_prsut123@hotmail", "sifra123");
            servis.UredjivanjeProfila(korisnik);
        }

             [TestMethod]
            public void UredjivanjeKorisnika_ValidniPodaci_Prolazi()
            {
                Korisnik noviKorisnik = new Korisnik(1, "adnan", "Adnan", "Pršut", "+38762123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
                Assert.AreEqual(noviKorisnik.ToString(), rez.ToString());

            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanID_DizeException()
            {
                Korisnik noviKorisnik = new Korisnik(55, "adnan", "Adnan", "Pršut", "+38762123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);

            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanUsername_DizeException()

            
            {

                Korisnik noviKorisnik = new Korisnik(1, "1adnan", "Adnan", "Pršut", "+38762123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanoIme_DizeException()
            {
                Korisnik noviKorisnik = new Korisnik(1, "adnan", "1Adnan", "Pršut", "+38762123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
            }

        
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UredjivanjeKorisnika_NeispravanoPrezime_DizeException()
            {

                Korisnik noviKorisnik = new Korisnik(1, "adnan", "Adnan", "1Pršut", "+38762123456", "adnan_prsut@gmail.com", "sifra123");
                Korisnik rez = servis.UredjivanjeProfila(noviKorisnik);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void TestPrekratkaSifre(){
                Korisnik korisnik = new Korisnik(1, "dandadan", "Adnan", "Pršut", "+38762654321", "adnan_prsut123@hotmail.com", "sif");
                servis.UredjivanjeProfila(korisnik);

            }
    }
}
    

