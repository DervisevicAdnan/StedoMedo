using StedoMedo.Data;
using StedoMedo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StedoMedo.Services.UpravljanjeTroskovima
{
    public class ServisUpravljanjeTroskovima : IServisUpravljanjeTroskovima
    {
        public IDbClass db;
        public ServisUpravljanjeTroskovima(IDbClass db)
        {
            this.db = db;
        }

        public bool DodajTrosak(Korisnik korisnik, double iznos, KategorijaTroska kategorijaTroska = KategorijaTroska.Ostalo, string opis = "", DateTime? dateTime = null)
        {
            try
            {
                DateTime datumIVrijeme = DateTime.Now;
                if (dateTime != null) datumIVrijeme = dateTime.Value;
                int id = (db.Troskovi.Count == 0)? 0 : db.Troskovi.Last().Id + 1;
                db.AddTrosak(new Trosak(id, korisnik, datumIVrijeme, iznos, kategorijaTroska, opis));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greska prilikom dodavanja troska!");
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public bool ObrisiTrosak(Korisnik korisnik, int idTroska)
        {
            try
            {
                var trosak = db.Troskovi.FirstOrDefault(t => t.Id == idTroska && t.Korisnik == korisnik);
                if (trosak == null) return false;

                db.Troskovi.Remove(trosak);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greska prilikom brisanja troska!");
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public bool IzmijeniTrosak(Korisnik korisnik, int idTroska, double iznos, KategorijaTroska kategorijaTroska)
        {
            try
            {
                var trosak = db.Troskovi.FirstOrDefault(t => t.Id == idTroska && t.Korisnik == korisnik);
                if (trosak == null) return false;

                trosak.Iznos = iznos;
                trosak.KategorijaTroska = kategorijaTroska;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greska prilikom izmjene troska!");
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public List<Trosak> DohvatiTroskove(Korisnik korisnik, ParametriFiltriranja parametriFiltriranja,
            List<KriterijSortiranja>? kriterijiSortiranja = null)
        {
                var troskovi = db.Troskovi.Where(t => FilterCondition(t, korisnik, parametriFiltriranja)).ToList();

                if (NotNullNotEmpty(kriterijiSortiranja))
                    troskovi.Sort((x,y) => {
                        foreach (KriterijSortiranja kriterij1 in kriterijiSortiranja)
                        {
                            int krit = kriterij1.KriterijPoredjenja(x, y, kriterij1.SmjerSortiranja);
                            if (krit != 0) return krit;
                        }
                        return 0;
                    });

                return troskovi;
        }

        private bool NotNullNotEmpty(List<KriterijSortiranja>? kriterijiSortiranja)
        {
            return kriterijiSortiranja != null && kriterijiSortiranja.Any();
        }

        private bool FilterCondition(Trosak t, Korisnik korisnik, ParametriFiltriranja parametriFiltriranja)
        {
            return t.Korisnik == korisnik &&
                    t.DatumIVrijeme >= parametriFiltriranja.odDatuma && t.DatumIVrijeme <= parametriFiltriranja.doDatuma &&
                    parametriFiltriranja.kategorijeTroskova.Contains(t.KategorijaTroska);
        }
    }
}
