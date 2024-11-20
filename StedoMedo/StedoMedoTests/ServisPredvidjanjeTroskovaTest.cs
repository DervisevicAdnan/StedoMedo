using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ServisPredvidjanjeTroskovaTest
{
    [TestClass]
    public class ServisPredvidjanjeTroskovaTest
    {
        private DbClass db;
        private ServisPredvidjanjeTroskova servis;
        private Korisnik korisnik1;
        private Korisnik korisnik2;
        private DateTime dt1;

        [TestInitialize]
        public void TestSetup()
        {
            db = new DbClass();
            servis = new ServisPredvidjanjeTroskova(db);
            korisnik1 = new Korisnik(1, "arman", "Arman", "Bašović", "061123456", "arman_baasovic@hotmail.com", "arman123");
            korisnik2 = new Korisnik(2, "hasim", "Hašim", "Kučuk", "061321654", "hasim_kucuk@hotmail.com", "hale1234");
            dt1 = new DateTime(2023, 12, 12, 5, 10, 20);

            db.AddKorisnik(korisnik1);
            db.AddKorisnik(korisnik2);

            for (int i = 0; i < 32; i++)
            {
                if (i % 2 == 1)
                    db.AddTrosak(new Trosak(i, korisnik1, dt1, i * 4.0 / 3.0, KategorijaTroska.Hrana, "Voće"));
                else
                    db.AddTrosak(new Trosak(i, korisnik2, dt1, 10, KategorijaTroska.Hrana, "Povrće"));

                dt1 = dt1.AddHours(6);
            }
        }
        [TestMethod]
        public void TestProcijene()
        {
            var procijena = servis.ProcijeniTroskove(korisnik1, 1);
            var ocekivanaVrijednost = 90.67;
            bool tacnost = Math.Abs(procijena - ocekivanaVrijednost) <= 1e-16;
            Assert.IsTrue(tacnost);
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestKorisnikBezTroškova()
        {
            var korisnik3= new Korisnik(3, "tarik", "Tarik", "Baždar", "061883225", "tarik_bazdar@hotmail.com", "tare123");
            var procijena = servis.ProcijeniTroskove(korisnik3, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativniDaniZaPredvidjanje()
        {
            var procijena = servis.ProcijeniTroskove(korisnik1, -1);
        }
    }
}
