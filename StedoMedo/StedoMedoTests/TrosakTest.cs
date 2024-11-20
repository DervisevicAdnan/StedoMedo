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
            Assert.AreEqual(korisnik.ToString(), "0 ado Adnan Dervisevic 387600000000 mail@gmail.com");
        }
    }
}
