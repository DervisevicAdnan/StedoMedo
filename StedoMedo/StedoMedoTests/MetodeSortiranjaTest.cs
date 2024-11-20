using StedoMedo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    [TestClass]
    public class MetodeSortiranjaTest
    {
        List<Trosak> troskovi;
        Korisnik korisnik;

        [TestInitialize]
        public void Postavi()
        {
            korisnik = new Korisnik(1, "ado", "Adnan", "Dervisevic", "387600000000", "mail@gmail.com", "nesto");
            troskovi = new List<Trosak>
            {
                new Trosak(0,korisnik,DateTime.Parse("01-01-2024"),50,KategorijaTroska.Rezije,""),
                new Trosak(1,korisnik,DateTime.Parse("01-02-2024"),60,KategorijaTroska.Izlasci,""),
                new Trosak(2,korisnik,DateTime.Parse("01-03-2024"),70,KategorijaTroska.Hrana,""),
                new Trosak(3,korisnik,DateTime.Parse("01-04-2024"),50,KategorijaTroska.Odjeca,""),
                new Trosak(4,korisnik,DateTime.Parse("01-02-2024"),30,KategorijaTroska.Prijevoz,"")
            };
        }

        [TestMethod]
        [DataRow(0, 0, SmjerSortiranja.Rastuci,0)]
        [DataRow(0, 0, SmjerSortiranja.Opadajuci,0)]
        [DataRow(0, 1, SmjerSortiranja.Rastuci,1)]
        [DataRow(1, 0, SmjerSortiranja.Rastuci,-1)]
        [DataRow(0, 1, SmjerSortiranja.Opadajuci, -1)]
        [DataRow(1, 0, SmjerSortiranja.Opadajuci, 1)]
        public void SortirajPoIdTest(int indeks1, int indeks2, SmjerSortiranja smjer, int rezultat)
        {
            int rez = MetodeSortiranja.SortirajPoId(troskovi[indeks1], troskovi[indeks2], smjer);

            Assert.AreEqual(rez, rezultat);
        }

        [TestMethod]
        [DataRow(1, 4, SmjerSortiranja.Rastuci, 0)]
        [DataRow(1, 4, SmjerSortiranja.Opadajuci, 0)]
        [DataRow(1, 2, SmjerSortiranja.Rastuci, 1)]
        [DataRow(2, 1, SmjerSortiranja.Rastuci, -1)]
        [DataRow(1, 2, SmjerSortiranja.Opadajuci, -1)]
        [DataRow(2, 1, SmjerSortiranja.Opadajuci, 1)]
        public void SortirajPoDatumuTest(int indeks1, int indeks2, SmjerSortiranja smjer, int rezultat)
        {
            int rez = MetodeSortiranja.SortirajPoDatumu(troskovi[indeks1], troskovi[indeks2], smjer);

            Assert.AreEqual(rez, rezultat);
        }

        [TestMethod]
        [DataRow(0, 3, SmjerSortiranja.Rastuci, 0)]
        [DataRow(0, 3, SmjerSortiranja.Opadajuci, 0)]
        [DataRow(1, 2, SmjerSortiranja.Rastuci, 1)]
        [DataRow(2, 1, SmjerSortiranja.Rastuci, -1)]
        [DataRow(1, 2, SmjerSortiranja.Opadajuci, -1)]
        [DataRow(2, 1, SmjerSortiranja.Opadajuci, 1)]
        public void SortirajPoIznosuTest(int indeks1, int indeks2, SmjerSortiranja smjer, int rezultat)
        {
            int rez = MetodeSortiranja.SortirajPoIznosu(troskovi[indeks1], troskovi[indeks2], smjer);

            Assert.AreEqual(rez, rezultat);
        }

        [TestMethod]
        [DataRow(0, 0, SmjerSortiranja.Rastuci, 0)]
        [DataRow(0, 0, SmjerSortiranja.Opadajuci, 0)]
        [DataRow(2, 1, SmjerSortiranja.Rastuci, 1)]
        [DataRow(1, 2, SmjerSortiranja.Rastuci, -1)]
        [DataRow(2, 1, SmjerSortiranja.Opadajuci, -1)]
        [DataRow(1, 2, SmjerSortiranja.Opadajuci, 1)]
        public void SortirajPoKategorijiTest(int indeks1, int indeks2, SmjerSortiranja smjer, int rezultat)
        {
            int rez = MetodeSortiranja.SortirajPoKategoriji(troskovi[indeks1], troskovi[indeks2], smjer);

            Assert.AreEqual(rez, rezultat);
        }
    }
}
