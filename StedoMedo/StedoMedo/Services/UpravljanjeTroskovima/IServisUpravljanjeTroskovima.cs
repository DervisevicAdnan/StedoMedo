﻿using StedoMedo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedo.Services.UpravljanjeTroskovima
{
    public interface IServisUpravljanjeTroskovima
    {
        public bool DodajTrosak(Korisnik korisnik, double iznos, KategorijaTroska kategorijaTroska = KategorijaTroska.Ostalo, string opis = "", DateTime? dateTime = null);
        public bool ObrisiTrosak(Korisnik korisnik, int idTroska);
        public bool IzmijeniTrosak(Korisnik korisnik, int idTroska, double iznos, KategorijaTroska kategorijaTroska);
        public List<Trosak> DohvatiTroskove(Korisnik korisnik, ParametriFiltriranja parametriFiltriranja, List<KriterijSortiranja>? kriterijiSortiranja = null);

    }
}
