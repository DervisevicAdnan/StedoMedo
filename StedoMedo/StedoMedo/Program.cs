using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services;
using StedoMedo.UI;
using System;

namespace StedoMedo
{
    public class Program
    {
        public static void Main()
        {
            var db = new DbClass();
            int korisnikId = db.GetNextKorisnikId(); 

            Console.WriteLine("Unesite informacije o korisniku:");

            Console.WriteLine($"ID korisnika: {korisnikId}"); 

            Console.Write("Korisničko ime: ");
            string korisnickoIme = Console.ReadLine();

            Console.Write("Ime: ");
            string ime = Console.ReadLine();

            Console.Write("Prezime: ");
            string prezime = Console.ReadLine();

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Lozinka: ");
            string lozinka = Console.ReadLine();

            Korisnik korisnik = new Korisnik(korisnikId, korisnickoIme, ime, prezime, telefon, email, lozinka);
            var budzetService = new BudzetService(db);

            Console.WriteLine("\nDobrodošli u Budžet Aplikaciju!");

            while (true)
            {
                Console.WriteLine("\nOdaberite željenu opciju:");
                Console.WriteLine("1. Dodaj budžet");
                Console.WriteLine("2. Obrisi budžet");
                Console.WriteLine("3. Izmjeni budžet");
                Console.WriteLine("4. Prikaži preostalo stanje budžeta");
                Console.WriteLine("Upišite 'exit' za izlaz");

                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Hvala na korištenju aplikacije!");
                    break;
                }

                int opcija;
                if (!int.TryParse(input, out opcija))
                {
                    Console.WriteLine("Nepoznata opcija. Pokušajte ponovo.");
                    continue;
                }

                switch (opcija)
                {
                    case 1:
                        KonzolaBudzet.DodajBudzet(budzetService, korisnik);
                        break;
                    case 2:
                        KonzolaBudzet.ObrisiBudzet(budzetService, korisnik);
                        break;
                    case 3:
                        KonzolaBudzet.IzmjeniBudzet(budzetService, korisnik);
                        break;
                    case 4:
                        KonzolaBudzet.PrikaziPreostaloStanje(budzetService, korisnik);
                        break;
                    default:
                        Console.WriteLine("Nepoznata opcija. Pokušajte ponovo.");
                        break;
                }
            }
        }
    }
}
