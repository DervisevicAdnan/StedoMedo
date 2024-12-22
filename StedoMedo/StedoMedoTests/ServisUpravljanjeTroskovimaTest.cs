using Microsoft.VisualStudio.TestPlatform.Utilities;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrikaziTroskove_SimuliranaGreska_VracaFalse()
        {
            dbClass.Troskovi = null;

            var rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja());

            //Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void PrikaziTroskove_KorisnikNemaTroskova_VracaTrue()
        {
            var rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja());

            //Assert.IsTrue(rezultat);
        }
        [TestMethod]
        public void PrikaziTroskove_BezFiltriranjaISortiranja_VracaTrue()
        {
            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", DateTime.Now);
            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 70);
            servis.DodajTrosak(korisnik, 54);
            var rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja());

            Assert.AreEqual(rezultat.Count, 4);
            Assert.AreEqual(rezultat[0].KategorijaTroska.ToString(), "Prijevoz");
            Assert.AreEqual(rezultat[1].KategorijaTroska.ToString(), "Ostalo");
            Assert.AreEqual(rezultat[3].Id, 3);
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
                        1,
                        "Prijevoz",
                        0,
                        3
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("20-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        null,
                        DateTime.Parse("10-11-2024"),
                        DateTime.Parse("14-11-2024"),
                        2,
                        "Hrana",
                        0,
                        1
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("20-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        null,
                        DateTime.Parse("14-11-2024"),
                        DateTime.Parse("14-11-2024"),
                        1,
                        "Hrana",
                        0,
                        1
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("20-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        new List<KategorijaTroska> {KategorijaTroska.Izlasci},
                        DateTime.Parse("10-01-2024"),
                        DateTime.Parse("14-01-2024"),
                        0,
                        "",
                        0,
                        0
                    },
                    new object[] {
                        new List<DateTime> { DateTime.Parse("01-11-2024"), DateTime.Parse("14-11-2024"), DateTime.Parse("12-11-2024"), DateTime.Parse("05-11-2024")},
                        new List<double> { 20, 70, 50, 54 },
                        new List<KategorijaTroska> {KategorijaTroska.Ostalo, KategorijaTroska.Hrana, KategorijaTroska.Rezije, KategorijaTroska.Prijevoz},
                        new List<KategorijaTroska> {KategorijaTroska.Prijevoz, KategorijaTroska.Hrana},
                        DateTime.Parse("10-11-2024"),
                        DateTime.Parse("14-11-2024"),
                        1,
                        "Hrana",
                        0,
                        1
                    }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TroskoviData))]
        public void PrikaziTroskove_SaFiltriranjem_VracaTrue(List<DateTime> datumi, List<double> iznosi, List<KategorijaTroska> kategorije,
            List<KategorijaTroska>? filtriranjeKategorije, DateTime? filterOdDatuma, DateTime? filterDoDatuma,
            int brLinija, string kategorija, int linija, int start)
        {

            for (int i = 0; i < datumi.Count; i++)
            {
                servis.DodajTrosak(korisnik, iznosi[i], kategorije[i], "", datumi[i]);
            }

            var rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja(filterOdDatuma, filterDoDatuma, filtriranjeKategorije));

            Assert.AreEqual(rezultat.Count, brLinija);
            if (brLinija > 0)
            {
                Assert.AreEqual(rezultat[0].KategorijaTroska.ToString(),kategorija);
                Assert.AreEqual(rezultat[linija].Id, start);
            }
        }

        [TestMethod]
        public void PrikaziTroskove_SaSortiranjem_VracaTrue()
        {
            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 70);
            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", DateTime.Now);
            servis.DodajTrosak(korisnik, 54);

            var rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja(), [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci)]);

            Assert.AreEqual(rezultat.Count, 4);
            Assert.AreEqual(rezultat[0].KategorijaTroska.ToString(),"Ostalo");
            Assert.AreEqual(rezultat[2].KategorijaTroska.ToString(), "Prijevoz");
            Assert.AreEqual(rezultat[0].Id, 1);
            Assert.AreEqual(rezultat[1].Id, 3);
            Assert.AreEqual(rezultat[2].Id, 2);
            Assert.AreEqual(rezultat[3].Id, 0);
        }

        [TestMethod]
        public void PrikaziTroskove_SortiranjePoViseKriterija_VracaTrue()
        {
            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 20, KategorijaTroska.Izlasci);
            servis.DodajTrosak(korisnik, 20, KategorijaTroska.Hrana);
            servis.DodajTrosak(korisnik, 20);
            servis.DodajTrosak(korisnik, 70);
            servis.DodajTrosak(korisnik, 50, KategorijaTroska.Prijevoz, "Gorivo", DateTime.Now);
            servis.DodajTrosak(korisnik, 54);

            var rezultat = servis.DohvatiTroskove(korisnik, new ParametriFiltriranja(),
                [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci), 
                new KriterijSortiranja(MetodeSortiranja.SortirajPoKategoriji)]);

            Assert.AreEqual(rezultat.Count, 7);
            Assert.AreEqual(rezultat[0].KategorijaTroska.ToString(), "Ostalo");
            Assert.AreEqual(rezultat[2].KategorijaTroska.ToString(), "Prijevoz");
            Assert.AreEqual(rezultat[3].KategorijaTroska.ToString(), "Hrana");
            Assert.AreEqual(rezultat[4].KategorijaTroska.ToString(), "Izlasci");
            Assert.AreEqual(rezultat[0].Id, 4);
            Assert.AreEqual(rezultat[1].Id, 6);
            Assert.AreEqual(rezultat[2].Id, 5);
            Assert.AreEqual(rezultat[3].Id, 2);
            Assert.AreEqual(rezultat[4].Id, 1);
            Assert.AreEqual(rezultat[5].Id, 0);
            Assert.AreEqual(rezultat[6].Id, 3);
        }
    }
}