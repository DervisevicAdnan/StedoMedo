using Moq;
using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    [TestClass]
    public class TDDIspisIzvjestajaTest
    {
        Mock<IDbClass> dbClassMock;
        Korisnik korisnik;
        ServisUpravljanjeTroskovima servis;

        [TestInitialize]
        public void Initialize()
        {
            // Kreiraj mock
            dbClassMock = new Mock<IDbClass>();

            // Mockuj svojstvo Korisnici
            dbClassMock.Setup(db => db.Korisnici).Returns(new List<Korisnik>
            {
                new Korisnik(0, "korisnik", "Korisnik", "Korisnikovic", "387600000000", "mail@gmail.com", "nesto")
            });

            // Testiraj dohvaćanje korisnika
            korisnik = dbClassMock.Object.Korisnici.FirstOrDefault(k => k.Id == 0);

            servis = new ServisUpravljanjeTroskovima(dbClassMock.Object);
        }

        [TestMethod]
        public void PrikaziTroskove_IspisujeIspravnoIzvjestaj_VracaTrue()
        {
            dbClassMock.Setup(db => db.Troskovi).Returns(new List<Trosak> {
                new Trosak(0,korisnik,DateTime.Now,20,KategorijaTroska.Ostalo,""),
                new Trosak(0,korisnik,DateTime.Now,20,KategorijaTroska.Izlasci,""),
                new Trosak(0,korisnik,DateTime.Now,20,KategorijaTroska.Hrana,""),
                new Trosak(0,korisnik,DateTime.Now,20,KategorijaTroska.Ostalo,""),
                new Trosak(0,korisnik,DateTime.Now,70,KategorijaTroska.Ostalo,""),
                new Trosak(0,korisnik,DateTime.Now,50,KategorijaTroska.Prijevoz,"Gorivo"),
                new Trosak(0,korisnik,DateTime.Now,54,KategorijaTroska.Ostalo,""),

            });


            bool rezultat = servis.PrikaziTroskove(korisnik, null, null, null,
                [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci),
                new KriterijSortiranja(MetodeSortiranja.SortirajPoKategoriji)], true);

            string testFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(testFolderPath, korisnik.Ime + korisnik.Prezime + "Izvjestaj.txt");

            Assert.IsTrue(File.Exists(filePath));



            string content = File.ReadAllText(filePath);
            var linije = content.Split(Environment.NewLine).ToList();

            Assert.IsTrue(rezultat);
            Assert.AreEqual(linije.Count, 10);
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
        }
    }
}
