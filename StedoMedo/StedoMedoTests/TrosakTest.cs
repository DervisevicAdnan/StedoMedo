using StedoMedo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    [TestClass]
    public class TrosakTest
    {
        [TestMethod]
        public void ToStringTest()
        {
            Korisnik korisnik = new Korisnik(0, "ado", "Adnan", "Dervisevic", "387600000000", "mail@gmail.com", "nesto");
            Trosak trosak = new Trosak(0, korisnik, DateTime.Parse("01-01-2001"), 50, KategorijaTroska.Rezije, "opis");
            Assert.AreEqual(trosak.ToString(), "0 0 01/01/2001 50 Rezije opis");
        }
    }
}
