using StedoMedo.Models;
using StedoMedo.Services.ServisAutentifikacija;
using StedoMedo.Services.Statistika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedo.UI
{
    public class KonzolaStatistika
    {

        private readonly IServisStatistika _servis;
        private readonly Korisnik Korisnik;
        public KonzolaStatistika(IServisStatistika servis, Korisnik korisnik)
        {
            _servis = servis;
            Korisnik = korisnik;
        }

        public void StartConsole()
        {

            Console.WriteLine("Prikaz svih statistika za odabranog korisnika:\n");
            DateTime odDatuma = UnosDatuma();
            DateTime doDatuma = UnosDatuma();
            List<KategorijaTroska> kategorije = UnosKategorija();


            PrikaziNajveciTrosak(Korisnik, odDatuma, doDatuma, kategorije);
            PrikaziProsjecnuPotrosnju(Korisnik, odDatuma, doDatuma, kategorije);
            PrikaziRaspodjeluPoKategorijama(Korisnik, odDatuma, doDatuma);
            PrikaziUkupniTrosak(Korisnik, odDatuma, doDatuma, kategorije);
            PrikaziVarijansuTroskova(Korisnik, odDatuma, doDatuma);

            Console.WriteLine("\nPritisnite Enter za izlaz.");
            Console.ReadLine();
        }

        private DateTime UnosDatuma()
        {
            DateTime datum;
            Console.WriteLine("Unesite krajnji datum(yyyy - MM - dd):");
            while (!DateTime.TryParse(Console.ReadLine(), out datum))
            {
                Console.WriteLine("Pogrešan unos datuma. Pokušajte ponovo (yyyy-MM-dd):");
            }
            return datum;

        }

        private List<KategorijaTroska> UnosKategorija()
        {
            List<KategorijaTroska> kategorije = new List<KategorijaTroska>();

            Console.WriteLine("Unesite željene kategorije troškova (odvojene zarezom):");
            Console.WriteLine("Dostupne kategorije: Hrana, Rezije, Prijevoz, Izlasci, Odjeca, Ostalo");

            string unos = Console.ReadLine();
            string[] uneseneKategorije = unos.Split(',');

            foreach (string nazivKategorije in uneseneKategorije)
            {
                if (Enum.TryParse(nazivKategorije.Trim(), true, out KategorijaTroska kategorija))
                {
                    kategorije.Add(kategorija);
                }
                else
                {
                    Console.WriteLine($"Kategorija '{nazivKategorije.Trim()}' nije validna.");
                }
            }

            return kategorije;
        }

        private void PrikaziNajveciTrosak(Korisnik korisnik, DateTime odDatuma, DateTime doDatuma, List<KategorijaTroska> kategorije)
            {
                double najveciTrosak = _servis.NajveciTrosak(korisnik, odDatuma, doDatuma, kategorije);
                Console.WriteLine($"Najveći trošak: {najveciTrosak:C}");
            }

            private void PrikaziProsjecnuPotrosnju(Korisnik korisnik, DateTime odDatuma, DateTime doDatuma, List<KategorijaTroska> kategorije)
            {
                double prosjecnaPotrosnja = _servis.ProsjecnaPotrosnja(korisnik, odDatuma, doDatuma, kategorije);
                Console.WriteLine($"Prosječna potrošnja: {prosjecnaPotrosnja:C}");
            }

            private void PrikaziRaspodjeluPoKategorijama(Korisnik korisnik, DateTime odDatuma, DateTime doDatuma)
            {
                var raspodjela = _servis.RaspodjelaPoKategorijama(korisnik, odDatuma, doDatuma);
                Console.WriteLine("Raspodjela troškova po kategorijama:");
                foreach (var par in raspodjela)
                {
                    Console.WriteLine($"Kategorija: {par.Key.ToString()} - Iznos: {par.Value:C}");
                }
            }

            private void PrikaziUkupniTrosak(Korisnik korisnik, DateTime odDatuma, DateTime doDatuma, List<KategorijaTroska> kategorije)
            {
                double ukupniTrosak = _servis.UkupniTrosak(korisnik, odDatuma, doDatuma, kategorije);
                Console.WriteLine($"Ukupni trošak: {ukupniTrosak:C}");
            }

            private void PrikaziVarijansuTroskova(Korisnik korisnik, DateTime odDatuma, DateTime doDatuma)
            {
                double varijansa = _servis.VarijansaTroskova(korisnik, odDatuma, doDatuma);
                Console.WriteLine($"Varijansa troškova: {varijansa:C}");
            }
        }
    




}
