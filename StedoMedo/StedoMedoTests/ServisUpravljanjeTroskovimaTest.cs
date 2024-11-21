using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;
using System;
using System.Globalization;

namespace StedoMedoTests
{
    [TestClass]
    public class ServisUpravljanjeTroskovimaTest
    {
        DbClass dbClass;
        Korisnik korisnik;
        ServisUpravljanjeTroskovima servis;

        [TestInitialize]
        public void Initialize()
        {
            dbClass = new DbClass();
            korisnik = new Korisnik(1, "ado", "Adnan", "Dervisevic", "387600000000", "mail@gmail.com", "nesto");
            dbClass.AddKorisnik(korisnik);
            servis = new ServisUpravljanjeTroskovima(dbClass);
        }

        [TestMethod]
        public void DodajTrosak_DefaultVrijednosti_VracaTrue()
        {
            bool rezultat = servis.DodajTrosak(korisnik, 50);
            
            Assert.IsTrue(rezultat);
            Assert.AreEqual(dbClass.Troskovi.Count(), 1);
            Assert.AreEqual(dbClass.Troskovi[0].Korisnik, korisnik);
            Assert.AreEqual(dbClass.Troskovi[0].Iznos, 50);
        }

        [TestMethod]
        public void DodajTrosak_SimuliranaGreska_VracaFalse()
        {
            dbClass.Troskovi = null;

            bool rezultat = servis.DodajTrosak(korisnik, 50);

            Assert.IsFalse(rezultat);

        }

        [TestMethod]
        public void DodajTrosak_SveVrijednosti_VracaTrue()
        {
            DateTime dateTime = DateTime.Now;
            bool rezultat = servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", dateTime);

            Assert.IsTrue(rezultat);
            Assert.AreEqual(dbClass.Troskovi.Count(), 1);
            Assert.AreEqual(dbClass.Troskovi[0].Korisnik, korisnik);
            Assert.AreEqual(dbClass.Troskovi[0].Iznos, 50);
            Assert.AreEqual(dbClass.Troskovi[0].KategorijaTroska, KategorijaTroska.Prijevoz);
            Assert.AreEqual(dbClass.Troskovi[0].Opis, "Gorivo");
            Assert.AreEqual(dbClass.Troskovi[0].DatumIVrijeme, dateTime);
        }

        [TestMethod]
        public void DodajTrosak_DvaTroska_VracaTrueTrue()
        {
            bool rezultat1 = servis.DodajTrosak(korisnik, 50);
            bool rezultat2 = servis.DodajTrosak(korisnik, 10);

            Assert.IsTrue(rezultat1);
            Assert.IsTrue(rezultat2);
            Assert.AreEqual(dbClass.Troskovi.Count(), 2);
            Assert.AreEqual(dbClass.Troskovi[0].Id, 0);
            Assert.AreEqual(dbClass.Troskovi[0].Korisnik, korisnik);
            Assert.AreEqual(dbClass.Troskovi[0].Iznos, 50);
            Assert.AreEqual(dbClass.Troskovi[1].Id, 1);
            Assert.AreEqual(dbClass.Troskovi[1].Korisnik, korisnik);
            Assert.AreEqual(dbClass.Troskovi[1].Iznos, 10);
            
        }

        [TestMethod]
        public void ObrisiTrosak_TrosakPostoji_VracaTrue()
        {
            servis.DodajTrosak(korisnik, 50);
            bool rezultat = servis.ObrisiTrosak(korisnik, 0);

            Assert.IsTrue(rezultat);
            Assert.AreEqual(dbClass.Troskovi.Count(), 0);
        }

        [TestMethod]
        public void ObrisiTrosak_TrosakNePripadaKorisniku_VracaFalse()
        {
            Korisnik korisnik2 = new Korisnik(1, "k2", "Korisnik", "Korisnik", "38761111111", "maaail@gmail.com", "VelikoM");
            servis.DodajTrosak(korisnik2, 50);
            bool rezultat = servis.ObrisiTrosak(korisnik, 0);

            Assert.IsFalse(rezultat);
            Assert.AreEqual(dbClass.Troskovi.Count(), 1);
        }

        [TestMethod]
        public void ObrisiTrosak_NemaTroskaSaID_VracaFalse()
        {
            dbClass.Troskovi.Add(new Trosak(1, korisnik, DateTime.Now, 50, KategorijaTroska.Hrana, ""));

            bool rezultat = servis.ObrisiTrosak(korisnik, 0);

            Assert.IsFalse(rezultat);
            Assert.AreEqual(dbClass.Troskovi.Count(), 1);
        }

        [TestMethod]
        public void ObrisiTrosak_TrosakNePostoji_VracaFalse()
        {
            bool rezultat = servis.ObrisiTrosak(korisnik, 0);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void ObrisiTrosak_SimuliranaGreska_VracaFalse()
        {
            dbClass.Troskovi = null;

            bool rezultat = servis.ObrisiTrosak(korisnik, 0);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void IzmijeniTrosak_SimuliranaGreska_VracaFalse()
        {
            dbClass.Troskovi = null;

            bool rezultat = servis.IzmijeniTrosak(korisnik, 0, 0, KategorijaTroska.Ostalo);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void IzmijeniTrosak_ValidneVrijednosti_VracaTrue()
        {
            DateTime dateTime = DateTime.Now;
            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", dateTime);
            bool rezultat = servis.IzmijeniTrosak(korisnik, 0, 55, KategorijaTroska.Ostalo);
            
            Assert.IsTrue(rezultat);
            Assert.AreEqual(dbClass.Troskovi.Count(), 1);
            Assert.AreEqual(dbClass.Troskovi[0].Korisnik, korisnik);
            Assert.AreEqual(dbClass.Troskovi[0].Iznos, 55);
            Assert.AreEqual(dbClass.Troskovi[0].KategorijaTroska, KategorijaTroska.Ostalo);
            Assert.AreEqual(dbClass.Troskovi[0].Opis, "Gorivo");
            Assert.AreEqual(dbClass.Troskovi[0].DatumIVrijeme, dateTime);
        }

        [TestMethod]
        public void IzmijeniTrosak_TrosakNePostoji_VracaFalse()
        {
            bool rezultat = servis.IzmijeniTrosak(korisnik, 0, 55, KategorijaTroska.Ostalo);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void IzmijeniTrosak_NemaTroskaSaID_VracaFalse()
        {
            dbClass.Troskovi.Add(new Trosak(1, korisnik, DateTime.Now, 50, KategorijaTroska.Hrana, ""));

            bool rezultat = servis.IzmijeniTrosak(korisnik, 0, 55, KategorijaTroska.Ostalo);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void PrikaziTroskove_SimuliranaGreska_VracaFalse()
        {
            dbClass.Troskovi = null;

            bool rezultat = servis.PrikaziTroskove(korisnik);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void PrikaziTroskove_KorisnikNemaTroskova_VracaTrue()
        {
            bool rezultat = servis.PrikaziTroskove(korisnik);

            Assert.IsTrue(rezultat);
        }
        [TestMethod]
        public void PrikaziTroskove_BezFiltriranjaISortiranja_VracaTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", DateTime.Now);
            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 70);
            servis.DodajTrosak(korisnik, 54);
            bool rezultat = servis.PrikaziTroskove(korisnik);
            var sOutput = output.ToString();

            Assert.IsTrue(rezultat);
            Assert.AreEqual(sOutput.Split(Environment.NewLine).ToList().Count, 7);
            Assert.IsTrue(sOutput.Contains("Ostalo"));
            StringAssert.StartsWith(sOutput.Split(Environment.NewLine)[5], "              3");
        }

        public static IEnumerable<object[]> TroskoviData
        {
            get
            {
                return new[]
                {

                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("05-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        new List<KategorijaTroska> {KategorijaTroska.Prijevoz},
                        null,
                        null,
                        4,
                        "Prijevoz",
                        2,
                        "              3"
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("20-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        null,
                        DateTime.Parse("10-11-2024"),
                        DateTime.Parse("14-11-2024"),
                        5,
                        "Hrana",
                        2,
                        "              1"
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("20-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        null,
                        DateTime.Parse("14-11-2024"),
                        DateTime.Parse("14-11-2024"),
                        4,
                        "Hrana",
                        2,
                        "              1"
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("20-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        new List<KategorijaTroska> {KategorijaTroska.Izlasci},
                        DateTime.Parse("10-01-2024"),
                        DateTime.Parse("14-01-2024"),
                        3,
                        "",
                        0,
                        ""
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("05-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        new List<KategorijaTroska> {KategorijaTroska.Prijevoz, KategorijaTroska.Hrana},
                        DateTime.Parse("10-11-2024"),
                        DateTime.Parse("14-11-2024"),
                        4,
                        "Hrana",
                        2,
                        "              1"
                    }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TroskoviData))]
        public void PrikaziTroskove_SaFiltriranjem_VracaTrue(List<DateTime> datumi, List<double> iznosi, List<KategorijaTroska> kategorije,
            List<KategorijaTroska>? filtriranjeKategorije, DateTime? filterOdDatuma, DateTime? filterDoDatuma,
            int brLinija, string kategorija, int linija, string start)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            for (int i = 0; i < datumi.Count; i++)
            {
                servis.DodajTrosak(korisnik, iznosi[i], kategorije[i], "", datumi[i]);
            }

            bool rezultat = servis.PrikaziTroskove(korisnik, filterOdDatuma, filterDoDatuma, filtriranjeKategorije);
            var sOutput = output.ToString();

            Assert.IsTrue(rezultat);
            Assert.AreEqual(sOutput.Split(Environment.NewLine).ToList().Count, brLinija);
            Assert.IsTrue(sOutput.Contains(kategorija));
            StringAssert.StartsWith(sOutput.Split(Environment.NewLine)[linija], start);
        }

        [TestMethod]
        public void PrikaziTroskove_SaSortiranjem_VracaTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 70);
            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", DateTime.Now);
            servis.DodajTrosak(korisnik, 54);

            bool rezultat = servis.PrikaziTroskove(korisnik, null, null, null, [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci)]);
            var sOutput = output.ToString();
            var linije = sOutput.Split(Environment.NewLine).ToList();

            Assert.IsTrue(rezultat);
            Assert.AreEqual(linije.Count, 7);
            Assert.IsTrue(sOutput.Contains("Ostalo"));
            Assert.IsTrue(sOutput.Contains("Prijevoz"));
            StringAssert.StartsWith(linije[2], "              1");
            StringAssert.StartsWith(linije[3], "              3");
            StringAssert.StartsWith(linije[4], "              2");
            StringAssert.StartsWith(linije[5], "              0");
        }

        [TestMethod]
        public void PrikaziTroskove_SortiranjePoViseKriterija_VracaTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 20, KategorijaTroska.Izlasci);
            servis.DodajTrosak(korisnik, 20, KategorijaTroska.Hrana);
            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 70);
            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", DateTime.Now);
            servis.DodajTrosak(korisnik, 54);

            bool rezultat = servis.PrikaziTroskove(korisnik, null, null, null, 
                [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci), 
                new KriterijSortiranja(MetodeSortiranja.SortirajPoKategoriji)]);
            var sOutput = output.ToString();
            var linije = sOutput.Split(Environment.NewLine).ToList();

            Assert.IsTrue(rezultat);
            Assert.AreEqual(linije.Count, 10);
            Assert.IsTrue(sOutput.Contains("Ostalo"));
            Assert.IsTrue(sOutput.Contains("Prijevoz"));
            Assert.IsTrue(sOutput.Contains("Hrana"));
            Assert.IsTrue(sOutput.Contains("Izlasci"));
            StringAssert.StartsWith(linije[2], "              4");
            StringAssert.StartsWith(linije[3], "              6");
            StringAssert.StartsWith(linije[4], "              5");
            StringAssert.StartsWith(linije[5], "              2");
            StringAssert.StartsWith(linije[6], "              1");
            StringAssert.StartsWith(linije[7], "              0");
            StringAssert.StartsWith(linije[8], "              3");
        }
    }
}