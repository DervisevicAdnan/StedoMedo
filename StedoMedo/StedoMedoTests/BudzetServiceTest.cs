using Moq;
using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services;
using StedoMedo.Services.UpravljanjeTroskovima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;


namespace StedoMedoTests
{
    [TestClass]

    public class BudzetServiceTest
    {
        private DbClass _db;
        private Mock<DbClass> _mockDb;
        private BudzetService _budzetService;
        private Mock<IBudzetService> _mockBudzetService;


        [TestInitialize]
         public void BudzetSetup()
         {
            _db = new DbClass();
            _mockDb = new Mock<DbClass>();
            _budzetService= new BudzetService(_db);
            _mockBudzetService = new Mock<IBudzetService>();
        }

        [TestMethod]

        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDodavanjaBudzeta_KorisnikNePostoji()
        {
            Korisnik korisnik = null;
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDodavanjaBudzeta_NegativanIznos()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = -2000;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
        }

        [TestMethod]
        public void TestDodavanjaBudzeta_UspjesnoDodan()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            var test = _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
            test.ToString();
            Assert.AreEqual(korisnik, test);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBrisanjeBudzeta_KorisnikNePostoji()
        {
            Korisnik korisnik = null;
            var id = 1;
            _budzetService.ObrisiBudzet(korisnik, id);
        }

        [TestMethod]
        public void TestBrisanjeBudzeta_BudzetNePostoji()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var id = 55;
            var test = _budzetService.ObrisiBudzet(korisnik, id);
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void TestBrisanjeBudzeta_BudzetPostoji()
        {            
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
            var id = 1;
            var test = _budzetService.ObrisiBudzet(korisnik, id);
            Assert.IsTrue(test);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIzmjeneBudzeta_KorisnikNePostoji()
        {
            Korisnik korisnik = null;
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            var id = 1;
            _budzetService.IzmjeniBudzet(korisnik, id, pocetak, kraj, iznos);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIzmjeneBudzeta_NegativanIznos()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = -2000;
            var id = 1;
            _budzetService.IzmjeniBudzet(korisnik, id, pocetak, kraj, iznos);
        }

        [TestMethod]
        public void TestIzmjeneBudzeta_NePostojiBudzet()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            var id = 1;
            var test = _budzetService.IzmjeniBudzet(korisnik, id, pocetak, kraj, iznos);
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void TestIzmjeneBudzeta_BudzetPostoji()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            var id = 1;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
            var novi_pocetak = DateOnly.Parse("2024-12-11");
            var novi_kraj = DateOnly.Parse("2024-12-30");
            var novi_iznos = 5000;
            var test = _budzetService.IzmjeniBudzet(korisnik, id, novi_pocetak, novi_kraj, novi_iznos);
            Assert.IsTrue(test);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPreostalogStanjaBudzeta_KorisnikNePostoji()
        {
            Korisnik korisnik = null;
            var datum = DateOnly.Parse("2024-12-12");
            _budzetService.PreostaloStanjeBudzeta(korisnik, datum);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestPreostalogStanjaBudzeta_BudzetNePostoji()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            _budzetService.PreostaloStanjeBudzeta(korisnik, DateOnly.Parse("2024-12-11"));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPreostalogStanjaBudzeta_DatumNijeValidan()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
            _budzetService.PreostaloStanjeBudzeta(korisnik, DateOnly.Parse("2024-12-12"));
        }

        [TestMethod]
        public void TestProvjeraPostojanja_Postoji()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
            var test = _budzetService.ProvjeraPostojanja(korisnik, 1);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void TestProvjeraPostojanja_NePostoji()
        {
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;
            _budzetService.DodajBudzet(korisnik, pocetak, kraj, iznos);
            var test = _budzetService.ProvjeraPostojanja(korisnik, 2);
            Assert.IsFalse(test);
        }

        [TestMethod]
        [DataRow(3000, 300, "2024-11-13", 2700)]
        [DataRow(3000, 450, "2024-11-18", 2550)] 
        [DataRow(3000, 550, "2024-11-30", 2450)] 
        public void TestPreostaloStanjeBudzeta_JedanBudzet(double ukupniBudzet, double ukupniTrosak, string doDanaStr, double expected)
        {
            ServisUpravljanjeTroskovima _trosakService = new ServisUpravljanjeTroskovima(_db);
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            DateOnly doDana = DateOnly.Parse(doDanaStr);
         
            _budzetService.DodajBudzet(korisnik, DateOnly.Parse("2024-11-01"), DateOnly.Parse("2024-11-30"),ukupniBudzet);
            _trosakService.DodajTrosak(korisnik, 300, KategorijaTroska.Hrana, "Kupovina hrane.",new DateTime(2024,11,12));
            _trosakService.DodajTrosak(korisnik, 150, KategorijaTroska.Rezije, "Placanje rezija.", new DateTime(2024, 11, 17));
            _trosakService.DodajTrosak(korisnik, 100, KategorijaTroska.Prijevoz, "Placanje prevoza", new DateTime(2024, 11, 28));
            var actual = _budzetService.PreostaloStanjeBudzeta(korisnik, doDana);

            Assert.AreEqual(expected, actual, "Ukupni budžet: {0}, Ukupni troškovi: {1}, do dana: {2}", ukupniBudzet, ukupniTrosak, doDana);
        }

        [TestMethod]
        [DataRow(3000, 300, "2024-11-13", 2700)]
        [DataRow(6000, 450, "2024-11-18", 5550)]
        [DataRow(6000, 550, "2024-11-30", 5450)]
        public void TestPreostaloStanjeBudzeta_ViseBudeta(double ukupniBudzet, double ukupniTrosak, string doDanaStr, double expected)
        {
            ServisUpravljanjeTroskovima _trosakService = new ServisUpravljanjeTroskovima(_db);
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            DateOnly doDana = DateOnly.Parse(doDanaStr);

            _budzetService.DodajBudzet(korisnik, DateOnly.Parse("2024-11-01"), DateOnly.Parse("2024-11-30"), 3000);
            _budzetService.DodajBudzet(korisnik, DateOnly.Parse("2024-11-15"), DateOnly.Parse("2024-11-30"), 3000);
            _trosakService.DodajTrosak(korisnik, 300, KategorijaTroska.Hrana, "Kupovina hrane.", new DateTime(2024, 11, 12));
            _trosakService.DodajTrosak(korisnik, 150, KategorijaTroska.Rezije, "Placanje rezija.", new DateTime(2024, 11, 17));
            _trosakService.DodajTrosak(korisnik, 100, KategorijaTroska.Prijevoz, "Placanje prevoza", new DateTime(2024, 11, 28));
            var actual = _budzetService.PreostaloStanjeBudzeta(korisnik, doDana);
            Assert.AreEqual(expected, actual, "Ukupni budžet: {0}, Ukupni troškovi: {1}, do dana: {2}", ukupniBudzet, ukupniTrosak, doDana);
        }

        public static IEnumerable<object[]> BudzetData
        {
            get
            {

                return new[]
                {
                    new object[] { 500, 20, "2024-12-04", 480 },
                    new object[] { 500, 60, "2024-12-18", 440 }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(BudzetData))]

        public void TestPreostalogStanjaBudzeta_DynamicData(double ukupniBudzet, double ukupniTrosak, string doDanaStr, double expected)
        {
            ServisUpravljanjeTroskovima _trosakService = new ServisUpravljanjeTroskovima(_db);
            Korisnik korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            DateOnly doDana = DateOnly.Parse(doDanaStr);

            _budzetService.DodajBudzet(korisnik, DateOnly.Parse("2024-12-01"), DateOnly.Parse("2024-12-30"), ukupniBudzet);
            _trosakService.DodajTrosak(korisnik, 20, KategorijaTroska.Hrana, "Kupovina hrane.", new DateTime(2024, 12, 4));
            _trosakService.DodajTrosak(korisnik, 40, KategorijaTroska.Rezije, "Placanje rezija.", new DateTime(2024, 12, 17));
            var actual = _budzetService.PreostaloStanjeBudzeta(korisnik, doDana);
            Assert.AreEqual(expected, actual, "Ukupni budžet: {0}, Ukupni troškovi: {1}, do dana: {2}", ukupniBudzet, ukupniTrosak, doDana);
        }

        [TestMethod]
        public void TestDodavanjaBudzeta_ZamjenskiObjekat()
        {
            var korisnik = new Korisnik(1, "adi", "Adi", "Drakovac", "062009537", "adrakovac2@etf.unsa.ba", "passhash");
            var pocetak = DateOnly.Parse("2024-11-11");
            var kraj = DateOnly.Parse("2024-11-30");
            var iznos = 2000;

            _mockBudzetService.Setup(service => service.DodajBudzet(It.IsAny<Korisnik>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<double>()))
                              .Returns(korisnik);

            var result = _mockBudzetService.Object.DodajBudzet(korisnik, pocetak, kraj, iznos);

            Assert.AreEqual(korisnik, result);
            _mockBudzetService.Verify(service => service.DodajBudzet(korisnik, pocetak, kraj, iznos), Times.Once);
        }
    }
}


