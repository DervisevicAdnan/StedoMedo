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
        }

        [TestMethod]
        public void TestPrikaziTroskoveWithMockKorisnik_CreatesFileWhenSpasiIzvjestajIsTrue()
        {
            dbClassMock.Setup(db => db.Troskovi).Returns(new List<Trosak> { });

            var odDatuma = new DateTime(2024, 1, 1);
            var doDatuma = new DateTime(2024, 12, 31);
            var kategorijeTroskova = new List<KategorijaTroska> { KategorijaTroska.Hrana, KategorijaTroska.Rezije };
            var kriterijiSortiranja = new List<KriterijSortiranja> { };

            bool result = servis.PrikaziTroskove(korisnik, odDatuma, doDatuma, kategorijeTroskova, kriterijiSortiranja, spasiIzvjestaj: true);

            Assert.IsTrue(result, "Metoda nije uspela!");

            string imeIzvjestaja = $"{korisnik.Ime}{korisnik.Prezime}Izvjestaj.txt";
            string putanja = Path.Combine(testFolderPath, imeIzvjestaja);

            Assert.IsTrue(File.Exists(putanja), "Datoteka nije kreirana!");
        }

        [TestMethod]
        public void TestPrikaziTroskoveWithMockKorisnik_DoesNotCreateFileWhenSpasiIzvjestajIsFalse()
        {
            dbClassMock.Setup(db => db.Troskovi).Returns(new List<Trosak> { });

            var odDatuma = new DateTime(2024, 1, 1);
            var doDatuma = new DateTime(2024, 12, 31);
            var kategorijeTroskova = new List<KategorijaTroska> { KategorijaTroska.Hrana, KategorijaTroska.Rezije };
            var kriterijiSortiranja = new List<KriterijSortiranja> { };

            bool result = servis.PrikaziTroskove(korisnik, odDatuma, doDatuma, kategorijeTroskova, kriterijiSortiranja, spasiIzvjestaj: false);

            Assert.IsTrue(result, "Metoda nije uspela!");

            string imeIzvjestaja = $"{korisnik.Ime}{korisnik.Prezime}Izvjestaj.txt";
            string putanja = Path.Combine(testFolderPath, imeIzvjestaja);

            Assert.IsFalse(File.Exists(putanja), "Datoteka je nepozvano kreirana!");
        }

    }
}
