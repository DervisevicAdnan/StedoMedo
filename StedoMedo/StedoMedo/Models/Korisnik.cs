using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedo.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string SifraHash { get; set; }

        public Korisnik(int id, string username, string ime, string prezime, string telefon, string email, string sifraHash)
        {
            Id = id;
            Username = username;
            Ime = ime;
            Prezime = prezime;
            Telefon = telefon;
            Email = email;
            SifraHash = sifraHash;
        }
        public override string ToString()
        {
            return Id.ToString() + " " + Username + " " + Ime + " " + Prezime + " " + Telefon + " " + Email;
        }
    }
}
