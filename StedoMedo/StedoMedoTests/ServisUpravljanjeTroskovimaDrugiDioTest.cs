using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedoTests
{
    [TestClass]
    public class ServisUpravljanjeTroskovimaDrugiDioTest
    {
        DbClass dbClass;
        ServisUpravljanjeTroskovima servis;

        [TestInitialize]
        public void Initialize()
        {
            dbClass = new DbClass();
            servis = new ServisUpravljanjeTroskovima(dbClass);
        }
        [TestMethod]
        public void UpravljanjeTroskovima_VisestrukiPozivi()
        {
            /*List<Korisnik> korisnik = [new Korisnik(0, "korisnik1", "Korisnik1", "Korisnik1", "387600000000", "mail@gmail.com", "nesto"),
                new Korisnik(0, "korisnik2", "Korisnik2", "Korisnik2", "387600000000", "mail@gmail.com", "nesto")];
            Random rnd = new Random();
            for (int i = 0; i < 25000; i++)
            {
                servis.DodajTrosak(korisnik[rnd.Next(0,1)], rnd.Next(1, 10000), (KategorijaTroska)rnd.Next(0, 5));
            }
            Console.SetOut(TextWriter.Null);
            int a = 0;
            for (int i = 0; i < 5000; i++)
            {
                bool rezultat = servis.DohvatiTroskove(korisnik[rnd.Next(0, 1)], null, null, [(KategorijaTroska)rnd.Next(0, 5)],
                    [new KriterijSortiranja(MetodeSortiranja.SortirajPoIznosu, SmjerSortiranja.Opadajuci),
                    new KriterijSortiranja(MetodeSortiranja.SortirajPoKategoriji)]);
            }
            for (int i = 0; i < 1000; i++)
            {
                bool rezultat = servis.DohvatiTroskove(korisnik[rnd.Next(0, 1)]);
            }
            a = 1;
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });*/
        }
    }
}
