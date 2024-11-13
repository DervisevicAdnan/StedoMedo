using StedoMedo.Models;
using StedoMedo.Data;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StedoMedo.Services.ServisAutentifikacija
{
    public class ServisAutentifikacija : IServisAutentifikacija
    {
        private readonly DbClass _db;

        public ServisAutentifikacija(DbClass db)
        {
            _db = db;
        }

        public bool KreirajKorisnika(string username, string ime, string prezime, string telefon, string email, string sifra)
        {
            if (_db.Korisnici.Any(k => k.Username == username))
            {
                Console.WriteLine("Korisnik sa tim korisničkim imenom već postoji");
                return false; 
            }

            string sifraHash = Hash(sifra);

            int noviId = _db.Korisnici.Any() ? _db.Korisnici.Max(k => k.Id) + 1 : 1;
            Korisnik noviKorisnik = new Korisnik(
                noviId,
                username,
                ime,
                prezime,
                telefon,
                email,
                sifraHash
            );

            _db.AddKorisnik(noviKorisnik);
            return true;
        }

        public Korisnik PrijaviKorisnika(string username, string sifra)
        {
            string sifraHash = Hash(sifra);

            return _db.Korisnici.FirstOrDefault(k => k.Username == username && k.SifraHash == sifraHash);
        }

        public bool OdjaviKorisnika(Korisnik user)
        {
            return _db.Korisnici.Contains(user);
        }

        public string Hash(string sifra)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sifra));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
