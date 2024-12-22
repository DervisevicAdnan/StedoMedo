using Moq;
using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    [TestClass]
    public class TDDIspisIzvjestajaTest
    {
        Mock<IDbClass> dbClassMock;
        Korisnik korisnik;
        ServisUpravljanjeTroskovima servis;
        string testFolderPath;

        [TestInitialize]
        public void Initialize()
        {
            dbClassMock = new Mock<IDbClass>();

            dbClassMock.Setup(db => db.Korisnici).Returns(new List<Korisnik>
            {
                new Korisnik(0, "korisnik", "Korisnik", "Korisnikovic", "387600000000", "mail@gmail.com", "nesto")
            });

            korisnik = dbClassMock.Object.Korisnici.FirstOrDefault(k => k.Id == 0);

            servis = new ServisUpravljanjeTroskovima(dbClassMock.Object);

            testFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (File.Exists(Path.Combine(testFolderPath, korisnik.Ime + korisnik.Prezime + "Izvjestaj.txt")))
            {
                // Brisanje fajla
                File.Delete(Path.Combine(testFolderPath, korisnik.Ime + korisnik.Prezime + "Izvjestaj.txt"));
            }
        }

        /*[TestMethod]
        public void TestPrikaziTroskoveWithMockKorisnik_CreatesFileWhenSpasiIzvjestajIsTrue()
        {

            dbClassMock.Setup(db => db.Troskovi).Returns(new List<Trosak> { });

            var odDatuma = new DateTime(2024, 1, 1);
            var doDatuma = new DateTime(2024, 12, 31);
            var kategorijeTroskova = new List<KategorijaTroska> { KategorijaTroska.Hrana, KategorijaTroska.Rezije };
            var kriterijiSortiranja = new List<KriterijSortiranja> { };

            bool result = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja(odDatuma, doDatuma, kategorijeTroskova), kriterijiSortiranja, spasiIzvjestaj: true);

            Assert.IsTrue(result, "Metoda nije uspela!");

            string imeIzvjestaja = $"{korisnik.Ime}{korisnik.Prezime}Izvjestaj.txt";
            string putanja = Path.Combine(testFolderPath, imeIzvjestaja);

            Assert.IsTrue(File.Exists(putanja), "Datoteka nije kreirana!");
        }*/

        /*[TestMethod]
        public void TestPrikaziTroskoveWithMockKorisnik_DoesNotCreateFileWhenSpasiIzvjestajIsFalse()

        {

            dbClassMock.Setup(db => db.Troskovi).Returns(new List<Trosak> { });

            var odDatuma = new DateTime(2024, 1, 1);
            var doDatuma = new DateTime(2024, 12, 31);
            var kategorijeTroskova = new List<KategorijaTroska> { KategorijaTroska.Hrana, KategorijaTroska.Rezije };
            var kriterijiSortiranja = new List<KriterijSortiranja> { };

            bool result = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja(odDatuma, doDatuma, kategorijeTroskova), kriterijiSortiranja, spasiIzvjestaj: false);

            Assert.IsTrue(result, "Metoda nije uspela!");

            string imeIzvjestaja = $"{korisnik.Ime}{korisnik.Prezime}Izvjestaj.txt";
            string putanja = Path.Combine(testFolderPath, imeIzvjestaja);

            Assert.IsFalse(File.Exists(putanja), "Datoteka je nepozvano kreirana!");
        }
        */

        /*[TestMethod]
        public void PrikaziTroskove_IspisujeIspravnoIzvjestaj_VracaTrue()
        {
            dbClassMock.Setup(db => db.Troskovi).Returns(new List<Trosak> {
                new Trosak(0,korisnik,DateTime.Now,20,KategorijaTroska.Ostalo,""),
                new Trosak(1,korisnik,DateTime.Now,20,KategorijaTroska.Izlasci,""),
                new Trosak(2,korisnik,DateTime.Now,20,KategorijaTroska.Hrana,""),
                new Trosak(3,korisnik,DateTime.Now,20,KategorijaTroska.Ostalo,""),
                new Trosak(4,korisnik,DateTime.Now,70,KategorijaTroska.Ostalo,""),
                new Trosak(5,korisnik,DateTime.Now,50,KategorijaTroska.Prijevoz,"Gorivo"),
                new Trosak(6,korisnik,DateTime.Now,54,KategorijaTroska.Ostalo,""),

            });

            bool rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja(),
                [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci),
                new KriterijSortiranja(MetodeSortiranja.SortirajPoKategoriji)], true);

            string filePath = Path.Combine(testFolderPath, korisnik.Ime + korisnik.Prezime + "Izvjestaj.txt");

            Assert.IsTrue(File.Exists(filePath));

            string content = File.ReadAllText(filePath);
            var linije = content.Split(Environment.NewLine).ToList();

            Assert.IsTrue(rezultat);
            Assert.AreEqual(linije.Count, 12);
            Assert.IsTrue(content.Contains("Ostalo"));
            Assert.IsTrue(content.Contains("Prijevoz"));
            Assert.IsTrue(content.Contains("Hrana"));
            Assert.IsTrue(content.Contains("Izlasci"));
            StringAssert.StartsWith(linije[4], "              4");
            StringAssert.StartsWith(linije[5], "              6");
            StringAssert.StartsWith(linije[6], "              5");
            StringAssert.StartsWith(linije[7], "              2");
            StringAssert.StartsWith(linije[8], "              1");
            StringAssert.StartsWith(linije[9], "              0");
            StringAssert.StartsWith(linije[10], "              3");
        }*/
    }
}
