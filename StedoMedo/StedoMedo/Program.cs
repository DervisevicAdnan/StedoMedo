// See https://aka.ms/new-console-template for more information

using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;
using StedoMedo.UI;

Korisnik k = new Korisnik(0, "ado", "Adnan", "Dervisevic", "0600000", "aa", "saagbrwe");
DbClass db = new DbClass();
IServisUpravljanjeTroskovima servisUpravljanjeTroskovima = new ServisUpravljanjeTroskovima(db);
KonzolaUpravljanjeTroskovima upravljanjeTroskovima = new KonzolaUpravljanjeTroskovima(servisUpravljanjeTroskovima,k);
upravljanjeTroskovima.PokreniInterakcijuKonzole();