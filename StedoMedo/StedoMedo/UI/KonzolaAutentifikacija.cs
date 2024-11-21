using StedoMedo.Models;
using StedoMedo.Services.ServisAutentifikacija;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StedoMedo.UI
{
    public class KonzolaAutentifikacija
    {

        private readonly IServisAutentifikacija _servis;

        public KonzolaAutentifikacija(IServisAutentifikacija servis)
        {
            _servis = servis;
        }

        public Korisnik StartConsole()
        {
            Console.Clear();
            DrawLogo();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Dobrodošli u");
            Thread.Sleep(500);


            string welcomeText = "$TEDOMEDO";
            ConsoleColor[] colors = {
            ConsoleColor.Red, ConsoleColor.DarkYellow, ConsoleColor.Green,
            ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta,
            ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkRed
        };

            for (int i = 0; i < welcomeText.Length; i++)
            {
                Console.ForegroundColor = colors[i % colors.Length];
                Console.Write(welcomeText[i]);
                Thread.Sleep(200);
            }

            Console.ResetColor();
            Console.WriteLine("\nHvala što ste sa nama!");
            Console.WriteLine();

            Thread.Sleep(600);

            Korisnik korisnik = null;
            while (true)
            {
                Console.WriteLine("1. Prijava postojećeg korisnika");
                Console.WriteLine("2. Registracija novog korisnika");
                Console.Write("Izaberite opciju: ");

                string izbor = Console.ReadLine() ?? "";
                switch (izbor)
                {
                    case "1":
                        bool success = Prijava(ref korisnik);
                        if (success)
                            return korisnik;
                        break;
                    case "2":
                        Registracija();

                        break;
                    default:
                        Console.WriteLine("Nevažeća opcija. Molimo pokušajte ponovo.");
                        break;
                }
            }
        }

        private IServisAutentifikacija Get_servis()
        {
            return _servis;
        }

        private bool Prijava(ref Korisnik? korisnik)
        {
            try
            {
                Console.Write("Unesite korisničko ime: ");
                string username = Console.ReadLine() ?? ""; ;

                Console.Write("Unesite lozinku: ");
                string password = GetPasswordInput();


                korisnik = _servis.PrijaviKorisnika(username, password);
                if (korisnik != null)
                {
                    Console.WriteLine("\nPrijava uspješna.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Prijava neuspješna. Molimo pokušajte ponovo.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }

            return false;
        }

        private void Registracija()
        {
            try
            {
                Console.Write("Unesite vaše ime: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Unesite vaše prezime: ");
                string surname = Console.ReadLine() ?? "";

                string phone;
                while (true)
                {
                    Console.Write("Unesite broj vašeg telefona u formatu (+387XXXXXXXX): ");
                    phone = Console.ReadLine() ?? "";

                    if (IsValidPhoneNumber(phone))
                    {
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Unesite validan broj telefona.");
                    }
                }

                string email;
                while (true)
                {
                    Console.Write("Unesite vašu email adresu: ");
                    email = Console.ReadLine() ?? "";

                    if (IsValidEmail(email))
                    {
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Unesite validnu email adresu.");
                    }
                }

                string username;
                while (true)
                {
                    Console.Write("Unesite korisničko ime: ");
                    username = Console.ReadLine() ?? "";

                    if (IsValidUsername(username))
                    {
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Korisničko ime nije validno. Mora početi slovom i može sadržavati samo slova i brojeve.");
                    }
                }

                string password = Lozinka();

                bool success = _servis.KreirajKorisnika(username, name, surname, phone, email, password);
                Console.WriteLine(success ? "\nRegistracija uspješna\n" : "\nRegistracija neuspješna. Molimo pokušajte ponovo.\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }
        }

        public bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^\+387\d{8,9}$";
            return Regex.IsMatch(phone, pattern);
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z][a-zA-Z0-9]*$";
            return Regex.IsMatch(username, pattern);
        }

        public bool IsValidPassword(string password)
        {
            string pattern = @"^.{8,30}$";
            return Regex.IsMatch(password, pattern);
        }

        public bool BrisanjeKorisnika(Korisnik korisnik)
        {
            try
            {
                Console.WriteLine("Za potvrdu brisanja korisničkog naloga unesite vašu lozinku: ");
                string password = _servis.Hash(GetPasswordInput());
                if (password == korisnik.SifraHash)
                {
                    bool obrisano = _servis.ObrisiKorisnika(korisnik);
                    if (obrisano)
                    {
                        Console.WriteLine("Profil uspješno obrisan");
                        return true;
                    }
                    Console.WriteLine("Brisanje profila nije uspjelo. Pokušajte ponovo.");
                }
                else
                {
                    Console.WriteLine("Pogrešna lozinka!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }
            return false;
        }

        private string Lozinka()
        {
            string password = null;
            string passworddb = null;

            do
            {
                Console.Write("Unesite lozinku: ");
                password = GetPasswordInput();

                if (!IsValidPassword(password))
                {
                    Console.WriteLine("\nLozinka nije validna. Mora imati između 8 i 30 karaktera.\n");
                    continue; 
                }

                Console.Write("Ponovo unesite lozinku: ");
                passworddb = GetPasswordInput();

                if (password != passworddb)
                {
                    Console.WriteLine("\nLozinke se ne poklapaju, pokušajte ponovo.\n");
                }

            } while (password != passworddb);

            return password;
        }

        private string GetPasswordInput()
        {
            string password = null;

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*");       
                }
                else if (password.Length > 0)  
                {
                    password = password.Substring(0, password.Length - 1);  
                    Console.Write("\b \b");   
                }
            }

            Console.WriteLine(); 

            return password;
        }

        public bool UredjivanjeProfila(Korisnik korisnik)
        {
            Console.WriteLine("Koje parametre želite promijeniti? (unesite odvojeno zarezima, npr. 'username, ime, prezime')");
            var parametriZaPromjenu = Console.ReadLine()?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim().ToLower()).ToList();

            if (parametriZaPromjenu == null || !parametriZaPromjenu.Any())
            {
                Console.WriteLine("Nijedan parametar nije odabran za promjenu.");
                return false;
            }

            foreach (var parametar in parametriZaPromjenu)
            {
                switch (parametar)
                {
                    case "username":
                        Console.WriteLine("Unesite novi username:");
                        korisnik.Username = Console.ReadLine();
                        break;
                    case "ime":
                        Console.WriteLine("Unesite novo ime:");
                        korisnik.Ime = Console.ReadLine();
                        break;
                    case "prezime":
                        Console.WriteLine("Unesite novo prezime:");
                        korisnik.Prezime = Console.ReadLine();
                        break;
                    case "brojtelefona":
                        Console.WriteLine("Unesite novi broj telefona:");
                        korisnik.Telefon = Console.ReadLine();
                        break;
                    case "email":
                        Console.WriteLine("Unesite novi email:");
                        korisnik.Email = Console.ReadLine();
                        break;
                    case "password":
                        Console.WriteLine("Unesite novi password:");
                        korisnik.SifraHash = _servis.Hash(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine($"Polje '{parametar}' nije podržano za promjenu.");
                        break;
                }
            }
            try
            {
               var k = _servis.UredjivanjeProfila(korisnik);
               Console.WriteLine("Sačuvane promjene u korisničkom profilu.");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }
            return false;
        }

        private void DrawLogo()
        {
            string logo = "" +
               "             +%=--=*:     :*@=-%+     -=:...:%@             \r\n           #=:.......-#%=:........:*@-:..:--:..-*           \r\n          *-.:=@%*=-..==...........:=-.:=#*+%=..:+          \r\n          ::.=======:..................-=======.:+          \r\n          ::.======.........:-...........=====:.:*          \r\n          -=-:===@=*%%%%@@@%#**#@@@@@@%%%*%+=:-=#           \r\n            *==@@@%===.  :=@@@@@@=--.  .==@@@=#.            \r\n               @@@....  . .+@==@@..--   ..*@@               \r\n               *%@....:==-.@@=-%@::=+-....*@=               \r\n              #-=@-..-@@%@#@==-=@@#%@@=..:@=:#:             \r\n             #=.:#@:....:+@@*###@@@:....:#@:.-#             \r\n             @=..:=#@@@@@=:@@@@@@+.%@@@@%=:..=@             \r\n              %=....-=:      +@      .=-....-@              \r\n               @=-:..=:       =       =:.:-=##:             \r\n            .+=========.      *      ========--==.          \r\n           +:.......:-===.         =+=-:.........-:         \r\n          ::..............=@@#*%@@@=.............:-         \r\n           -:..............-*.   *=..............=+         \r\n           +=-...........==--=- @=-==..........-=+          \r\n            :===.....:==:.===== #===-:==-....===%           \r\n              -@===-..=====@=      *====:.==@@+             \r\n                   #===+@@#         .-#===%                 "
           ;
               Console.WriteLine(logo);    
        }

    }

}
