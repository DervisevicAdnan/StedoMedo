using Moq;
using StedoMedo.Data;
using StedoMedo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    [TestClass]
    public class TDDUredjivanjeProfilaTest
    {
        [TestMethod]
        public void proba()
        {
            // Kreiraj mock
            var dbClassMock = new Mock<IDbClass>();

            // Mockuj svojstvo Korisnici
            dbClassMock.Setup(db => db.Korisnici).Returns(new List<Korisnik>
            {
                new Korisnik(1,"","Ivan","Markić","","","")
            });

            // Testiraj dohvaćanje korisnika
            var korisnik = dbClassMock.Object.Korisnici.FirstOrDefault(k => k.Id == 1);

            Assert.IsNotNull(korisnik);
            Assert.AreEqual("Ivan", korisnik.Ime);
        }
    }
}
