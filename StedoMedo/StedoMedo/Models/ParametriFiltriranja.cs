using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StedoMedo.Models
{
    public class ParametriFiltriranja
    {
        public DateTime odDatuma {  get; set; }
        public DateTime doDatuma { get; set; }
        public List<KategorijaTroska> kategorijeTroskova {  get; set; }

        public ParametriFiltriranja() 
        {
            odDatuma = DateTime.MinValue;
            doDatuma = DateTime.Now;
            kategorijeTroskova = [
                    KategorijaTroska.Hrana,
                        KategorijaTroska.Rezije,
                        KategorijaTroska.Prijevoz,
                        KategorijaTroska.Izlasci,
                        KategorijaTroska.Odjeca,
                        KategorijaTroska.Ostalo
                ];
        }
        public ParametriFiltriranja(DateTime? odDatuma = null, DateTime? doDatuma = null, List<KategorijaTroska>? kategorijeTroskova = null)
        {
            if (kategorijeTroskova == null || !kategorijeTroskova.Any())
                kategorijeTroskova = [
                    KategorijaTroska.Hrana,
                        KategorijaTroska.Rezije,
                        KategorijaTroska.Prijevoz,
                        KategorijaTroska.Izlasci,
                        KategorijaTroska.Odjeca,
                        KategorijaTroska.Ostalo
                ];
            if (odDatuma == null) this.odDatuma = DateTime.MinValue;
            else this.odDatuma = (DateTime)odDatuma;
            if (doDatuma == null) this.doDatuma = DateTime.Now;
            else this.doDatuma = (DateTime)doDatuma;
            this.kategorijeTroskova = kategorijeTroskova;
        }
    }
}
