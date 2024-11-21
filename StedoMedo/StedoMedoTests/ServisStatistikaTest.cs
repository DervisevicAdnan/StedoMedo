using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.Statistika;

namespace StedoMedoTests
{
    [TestClass]
    public class ServisStatistikaTests
    {
        private DbClass db;
        private ServisStatistika servis;
        private Korisnik korisnik;
        private List<Trosak> troskovi;

        [TestInitialize]
        public void TestInitialize()
        {
            db = new DbClass();
            servis = new ServisStatistika(db);
            korisnik = new Korisnik(55, "ali", "Ali", "Ljaljak", "387600000000", "mail@gmail.com", "nesto");
            db.AddKorisnik(korisnik);

            troskovi = new List<Trosak>
            {
                new Trosak(0, korisnik, DateTime.Parse("01-01-2024"), 50, KategorijaTroska.Rezije, ""),
                new Trosak(1, korisnik, DateTime.Parse("01-02-2024"), 60, KategorijaTroska.Izlasci, ""),
                new Trosak(2, korisnik, DateTime.Parse("01-03-2024"), 70, KategorijaTroska.Hrana, ""),
                new Trosak(3, korisnik, DateTime.Parse("01-04-2024"), 50, KategorijaTroska.Odjeca, ""),
                new Trosak(4, korisnik, DateTime.Parse("01-02-2024"), 30, KategorijaTroska.Prijevoz, "")
            };

            foreach (var trosak in troskovi)
            {
                db.AddTrosak(trosak);
            }
        }

        [TestMethod]
        public void NajveciTrosak_ValidInput_ReturnsCorrectResult()
        {
            var odDatuma = DateTime.Parse("31-12-2023");
            var doDatuma = DateTime.Now;
            var kategorije = new List<KategorijaTroska> { KategorijaTroska.Hrana, KategorijaTroska.Izlasci };

            double result = servis.NajveciTrosak(korisnik, odDatuma, doDatuma, kategorije);

            Assert.AreEqual(70, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void NajveciTrosak_InvalidDate_ThrowsException()
        {
            var doDatuma = DateTime.Now;
            var kategorije = new List<KategorijaTroska> { KategorijaTroska.Hrana, KategorijaTroska.Izlasci };
            servis.NajveciTrosak(korisnik, DateTime.Parse("00-00-0000"), doDatuma, kategorije);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NajveciTrosak_NullKorisnik_ThrowsArgumentNullException()
        {
            var kategorije = new List<KategorijaTroska> { KategorijaTroska.Hrana };

            servis.NajveciTrosak(null, DateTime.Parse("01-01-2024"), DateTime.Parse("01-02-2024"), kategorije);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NajveciTrosak_InvalidDateRange_ThrowsArgumentException()
        {
            var kategorije = new List<KategorijaTroska> { KategorijaTroska.Hrana };

            servis.NajveciTrosak(korisnik, DateTime.Parse("01-03-2024"), DateTime.Parse("01-01-2024"), kategorije);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NajveciTrosak_OutOfRangeDates_ThrowsArgumentOutOfRangeException()
        {
            var kategorije = new List<KategorijaTroska> { KategorijaTroska.Hrana };

            servis.NajveciTrosak(korisnik, DateTime.Parse("01-01-1999"), DateTime.Parse("01-02-2024"), kategorije);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NajveciTrosak_EmptyKategorije_ThrowsArgumentException()
        {
            servis.NajveciTrosak(korisnik, DateTime.Parse("01-01-2024"), DateTime.Parse("01-02-2024"), new List<KategorijaTroska>());
        }

        [DataTestMethod]
        [DataRow("31-12-2023", "01-04-2024", new[] { KategorijaTroska.Hrana, KategorijaTroska.Rezije }, 70)]
        [DataRow("01-01-2024", "01-02-2024", new[] { KategorijaTroska.Rezije }, 50)]
        public void NajveciTrosak_DataDrivenTests(string start, string end, KategorijaTroska[] kategorije, double expected)
        {
            var odDatuma = DateTime.Parse(start);
            var doDatuma = DateTime.Parse(end);
            var kategorijeList = new List<KategorijaTroska>(kategorije);

            double result = servis.NajveciTrosak(korisnik, odDatuma, doDatuma, kategorijeList);

            Assert.AreEqual(expected, result);
        }


        [DataTestMethod]
        [DataRow("31-12-2023", "01-04-2024", new[] { KategorijaTroska.Hrana, KategorijaTroska.Rezije }, 60)] 
        [DataRow("01-01-2024", "01-02-2024", new[] { KategorijaTroska.Rezije }, 50)]
        public void ProsjecnaPotrosnja_DataDrivenTests(string start, string end, KategorijaTroska[] kategorije, double expected)
        {
            var odDatuma = DateTime.Parse(start);
            var doDatuma = DateTime.Parse(end);
            var kategorijeList = new List<KategorijaTroska>(kategorije);

            double result = servis.ProsjecnaPotrosnja(korisnik, odDatuma, doDatuma, kategorijeList);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [DataRow(null, "01-01-2024", "01-04-2024", new KategorijaTroska[] { KategorijaTroska.Rezije })]
        [DataRow("01-04-2024", "01-01-2024", new KategorijaTroska[] { KategorijaTroska.Rezije })]
        [DataRow("01-01-1999", "01-04-2024", new KategorijaTroska[] { KategorijaTroska.Rezije })]
        [DataRow("01-01-2024", "01-04-2024", null)]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void ProsjecnaPotrosnja_InvalidInputs_ThrowsException(string odDatuma, string doDatuma, KategorijaTroska[] kategorije)
        {
           
            var fromDate = DateTime.Parse(odDatuma);
            var toDate = DateTime.Parse(doDatuma);
            var kategorijeList = kategorije?.ToList();

            
            servis.ProsjecnaPotrosnja(korisnik, fromDate, toDate, kategorijeList);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProsjecnaPotrosnja_NullKorisnik_ThrowsArgumentNullException()
        {
           
            DateTime fromDate = DateTime.Parse("01-01-2024");
            DateTime toDate = DateTime.Parse("01-04-2024");
            var kategorijeList = new List<KategorijaTroska> { KategorijaTroska.Rezije };

            
            servis.ProsjecnaPotrosnja(null, fromDate, toDate, kategorijeList);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProsjecnaPotrosnja_InvalidDateRange_ThrowsArgumentException()
        {
           
            DateTime fromDate = DateTime.Parse("01-04-2024");
            DateTime toDate = DateTime.Parse("01-01-2024");
            var kategorijeList = new List<KategorijaTroska> { KategorijaTroska.Rezije };

            
            servis.ProsjecnaPotrosnja(korisnik, fromDate, toDate, kategorijeList);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProsjecnaPotrosnja_InvalidDateRangeOutOfBounds_ThrowsArgumentOutOfRangeException()
        {
           
            DateTime fromDate = DateTime.Parse("01-01-1999");
            DateTime toDate = DateTime.Parse("01-04-2024");
            var kategorijeList = new List<KategorijaTroska> { KategorijaTroska.Rezije };

            
            servis.ProsjecnaPotrosnja(korisnik, fromDate, toDate, kategorijeList);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProsjecnaPotrosnja_EmptyCategoryList_ThrowsArgumentException()
        {
           
            DateTime fromDate = DateTime.Parse("01-01-2024");
            DateTime toDate = DateTime.Parse("01-04-2024");
            List<KategorijaTroska> kategorijeList = null;

            
            servis.ProsjecnaPotrosnja(korisnik, fromDate, toDate, kategorijeList);
        }
        [TestMethod]
        [DataRow("01-01-2024", "01-04-2024", new KategorijaTroska[] { KategorijaTroska.Rezije, KategorijaTroska.Hrana }, 120)]
        [DataRow("01-01-2024", "01-02-2024", new KategorijaTroska[] { KategorijaTroska.Izlasci, KategorijaTroska.Prijevoz }, 90)]
        [DataRow("01-03-2024", "01-04-2024", new KategorijaTroska[] { KategorijaTroska.Hrana, KategorijaTroska.Odjeca }, 120)]
        [DataRow("01-01-2024", "01-01-2024", new KategorijaTroska[] { KategorijaTroska.Rezije }, 50)]
        [DataRow("01-01-2024", "01-01-2024", new KategorijaTroska[] { KategorijaTroska.Hrana }, 0)]
        public void UkupniTrosak_ValidInputs_ReturnsCorrectTotal(string odDatuma, string doDatuma, KategorijaTroska[] kategorije, double expected)
        {
           
            var fromDate = DateTime.Parse(odDatuma);
            var toDate = DateTime.Parse(doDatuma);
            var kategorijeList = kategorije.ToList();

            
            double result = servis.UkupniTrosak(korisnik, fromDate, toDate, kategorijeList);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(null, "01-01-2024", "01-04-2024", new KategorijaTroska[] { KategorijaTroska.Rezije })]
        [DataRow("01-04-2024", "01-01-2024", new KategorijaTroska[] { KategorijaTroska.Rezije })]
        [DataRow("01-01-1999", "01-04-2024", new KategorijaTroska[] { KategorijaTroska.Rezije })]
        [DataRow("01-01-2024", "01-04-2024", null)]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void UkupniTrosak_InvalidInputs_ThrowsException(string odDatuma, string doDatuma, KategorijaTroska[] kategorije)
        {
           
            var fromDate = DateTime.Parse(odDatuma);
            var toDate = DateTime.Parse(doDatuma);
            var kategorijeList = kategorije?.ToList();

            
            servis.UkupniTrosak(korisnik, fromDate, toDate, kategorijeList);
        }
        [TestMethod]
        public void RaspodjelaPoKategorijama_ValidInput_ReturnsCorrectDistribution()
        {
            var odDatuma = DateTime.Parse("31-12-2023");
            var doDatuma = DateTime.Now;

            var result = servis.RaspodjelaPoKategorijama(korisnik, odDatuma, doDatuma);

            Assert.AreEqual(50, result[KategorijaTroska.Rezije]);
            Assert.AreEqual(60, result[KategorijaTroska.Izlasci]);
            Assert.AreEqual(70, result[KategorijaTroska.Hrana]);
            Assert.AreEqual(50, result[KategorijaTroska.Odjeca]);
            Assert.AreEqual(30, result[KategorijaTroska.Prijevoz]);
        }
        /*[TestMethod]
        [DataRow("01-01-2024", "01-04-2024", new[] { KategorijaTroska.Rezije, KategorijaTroska.Hrana }, 50, 70)]
        [DataRow("01-01-2024", "01-02-2024", new[] { KategorijaTroska.Izlasci, KategorijaTroska.Prijevoz }, 60, 30)]
        [DataRow("01-03-2024", "01-04-2024", new[] { KategorijaTroska.Hrana, KategorijaTroska.Odjeca }, 70, 50)]
        public void RaspodjelaPoKategorijama_ValidInputs_ReturnsCorrectCategoryDistribution(string odDatuma, string doDatuma, KategorijaTroska[] kategorije, double expectedRezije, double expectedIzlasci)
        {
           
            var fromDate = DateTime.Parse(odDatuma);
            var toDate = DateTime.Parse(doDatuma);

            
            var result = servis.RaspodjelaPoKategorijama(korisnik, fromDate, toDate);

            // Assert
            Assert.IsTrue(result.ContainsKey(KategorijaTroska.Rezije));
            Assert.AreEqual(expectedRezije, result[KategorijaTroska.Rezije]);

            Assert.IsTrue(result.ContainsKey(KategorijaTroska.Izlasci));
            Assert.AreEqual(expectedIzlasci, result[KategorijaTroska.Izlasci]);
        }*/

        [TestMethod]
        [DataRow(null, "01-01-2024", "01-04-2024")]
        [DataRow("01-04-2024", "01-01-2024")]
        [DataRow("01-01-1999", "01-04-2024")]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void RaspodjelaPoKategorijama_InvalidInputs_ThrowsException(string odDatuma, string doDatuma)
        {
            var fromDate = DateTime.Parse(odDatuma);
            var toDate = DateTime.Parse(doDatuma);

            servis.RaspodjelaPoKategorijama(korisnik, fromDate, toDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaspodjelaPoKategorijama_NullKorisnik_ThrowsArgumentNullException()
        {
           
            DateTime fromDate = DateTime.Parse("01-01-2024");
            DateTime toDate = DateTime.Parse("01-04-2024");

            
            servis.RaspodjelaPoKategorijama(null, fromDate, toDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaspodjelaPoKategorijama_InvalidDateRange_ThrowsArgumentException()
        {
           
            DateTime fromDate = DateTime.Parse("01-04-2024");
            DateTime toDate = DateTime.Parse("01-01-2024");

            
            servis.RaspodjelaPoKategorijama(korisnik, fromDate, toDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RaspodjelaPoKategorijama_InvalidDateRangeOutOfBounds_ThrowsArgumentOutOfRangeException()
        {
           
            DateTime fromDate = DateTime.Parse("01-01-1999");
            DateTime toDate = DateTime.Parse("01-04-2024");

            
            servis.RaspodjelaPoKategorijama(korisnik, fromDate, toDate);
        }
        [TestMethod]
        public void VarijansaTroskova_ValidInput_ReturnsCorrectVariance()
        {
            var odDatuma = DateTime.Parse("31-12-2023");
            var doDatuma = DateTime.Now;

            double result = servis.VarijansaTroskova(korisnik, odDatuma, doDatuma);

            Assert.AreEqual(176, Math.Round(result)); 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VarijansaTroskova_ShouldThrowArgumentNullException_WhenKorisnikIsNull()
        {
            servis.VarijansaTroskova(null, DateTime.Parse("01-01-2024"), DateTime.Parse("01-12-2024"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VarijansaTroskova_ShouldThrowArgumentException_WhenOdDatumaIsAfterDoDatuma()
        {
            servis.VarijansaTroskova(korisnik, DateTime.Parse("01-02-2024"), DateTime.Parse("01-01-2024"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VarijansaTroskova_ShouldThrowArgumentOutOfRangeException_WhenOdDatumaIsBefore2000()
        {
            servis.VarijansaTroskova(korisnik, DateTime.Parse("01-01-1999"), DateTime.Parse("01-12-2024"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VarijansaTroskova_ShouldThrowArgumentOutOfRangeException_WhenDoDatumaIsInTheFuture()
        {
            servis.VarijansaTroskova(korisnik, DateTime.Parse("01-01-2024"), DateTime.Parse("01-01-2025"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VarijansaTroskova_ShouldThrowArgumentException_WhenNoTroskoviInDateRange()
        {
            servis.VarijansaTroskova(korisnik, DateTime.Parse("01-05-2025"), DateTime.Parse("01-06-2025"));
        }
    }
}
