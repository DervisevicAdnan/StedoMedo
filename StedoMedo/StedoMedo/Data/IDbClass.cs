using StedoMedo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedo.Data
{
    public interface IDbClass
    {
        List<Korisnik> Korisnici { get; set; }
        List<Trosak> Troskovi { get; set; }
        List<Budzet> Budzeti { get; set; }
        void AddKorisnik(Korisnik korisnik);
        void AddTrosak(Trosak trosak);
        void AddBudzet(Budzet budzet);
    }
}
