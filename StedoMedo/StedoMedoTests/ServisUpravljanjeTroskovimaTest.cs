using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;

namespace StedoMedoTests
{
    [TestClass]
    public class ServisUpravljanjeTroskovimaTest
    {
        [TestMethod]
        public void TestDodajTrosak()
        {
            DbClass dbClass = new DbClass();
            Korisnik korisnik = new Korisnik(1, "ado", "Adnan", "Dervisevic", "387600000000", "mail@gmail.com", "nesto");
            dbClass.AddKorisnik(korisnik);
            ServisUpravljanjeTroskovima servis = new ServisUpravljanjeTroskovima(dbClass);
            servis.DodajTrosak(korisnik, 50);
            Assert.AreEqual(dbClass.Troskovi.Count(), 1);
            Assert.AreEqual(dbClass.Troskovi[0].Korisnik, korisnik);
            Assert.AreEqual(dbClass.Troskovi[0].Iznos, 50);
        }
    }
}