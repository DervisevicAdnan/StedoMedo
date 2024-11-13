﻿// See https://aka.ms/new-console-template for more information



using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.UpravljanjeTroskovima;
using StedoMedo.UI;

Korisnik k = new Korisnik(0, "ado", "Adnan", "Dervisevic", "0600000", "aa", "saagbrwe");
DbClass db = new DbClass();
IServisUpravljanjeTroskovima servisUpravljanjeTroskovima = new ServisUpravljanjeTroskovima(db);
KonzolaUpravljanjeTroskovima upravljanjeTroskovima = new KonzolaUpravljanjeTroskovima(servisUpravljanjeTroskovima,k);
upravljanjeTroskovima.PokreniInterakcijuKonzole();

/*using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services;
using System.Runtime.CompilerServices;


var db = new DbClass();
var servis=new ServisPredvidjanjeTroskova(db);
Korisnik test = new Korisnik(1, "arman", "Arman", "Bašović", "061123456", "arman_baasovic@hotmail.com", "arman123");
Korisnik test1= new Korisnik(2, "hasim", "Hašim", "Kučuk", "061321654", "hasim_kucuk@hotmail.com", "hale1234");
DateTime dt1 = new DateTime(2023, 12, 12, 5, 10, 20);
db.AddKorisnik(test);
for (int i = 0; i < 32; i++)
{
    if(i%2==1)
    db.AddTrosak(new Trosak(i, test, dt1, i*4.0/3.0, KategorijaTroska.Hrana, "Voće"));
    else
    db.AddTrosak(new Trosak(i, test1, dt1, 10, KategorijaTroska.Hrana, "Povrće"));

    dt1 =dt1.AddHours(6);
}
var procijena = servis.ProcijeniTroskove(test, 1);
if(procijena>=0)
Console.WriteLine(servis.ProcijeniTroskove(test, 1)+" novčanih jedinica");*/

/*using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services.Statistika;

DbClass db = new DbClass();


//int id, string username, string ime, string prezime, string telefon, string email, string sifraHash
Korisnik korisnik = new Korisnik(17,"ali1","Ali", "Laljak", "06023424","aljaljak2@etf.unsa.ba", "csdcodsaug2427" );
db.AddKorisnik(korisnik);
//int id, Korisnik korisnik, DateTime datumIVrijeme, double iznos, KategorijaTroska kategorijaTroska, string opis
db.AddTrosak(new Trosak(1, korisnik, DateTime.Now.AddDays(-10), 100,KategorijaTroska.Rezije , "Struja"));
db.AddTrosak(new Trosak(2, korisnik, DateTime.Now.AddDays(-5), 200, KategorijaTroska.Hrana, "Namirnice"));
db.AddTrosak(new Trosak(3, korisnik, DateTime.Now.AddDays(-2), 50, KategorijaTroska.Hrana, "Brza hrana"));


ServisStatistika servis = new ServisStatistika(db);


DateTime odDatuma = DateTime.Now.AddDays(-15);
DateTime doDatuma = DateTime.Now;


Console.WriteLine("Testing methods...");

double najveciTrosak = servis.NajveciTrosak(korisnik, odDatuma, doDatuma, new List<KategorijaTroska> {KategorijaTroska.Hrana });
Console.WriteLine($"Najveći trošak: {najveciTrosak}");

double prosjecnaPotrosnja = servis.ProsjecnaPotrosnja(korisnik, odDatuma, doDatuma, new List<KategorijaTroska> { KategorijaTroska.Rezije, KategorijaTroska.Hrana });
Console.WriteLine($"Prosječna potrošnja: {prosjecnaPotrosnja}");

Dictionary<KategorijaTroska, double> raspodjela = servis.RaspodjelaPoKategorijama(korisnik, odDatuma, doDatuma);
Console.WriteLine("Raspodjela po kategorijama:");
foreach (var item in raspodjela)
{
    Console.WriteLine($"Kategorija: {item.Key}, Iznos: {item.Value}");
}

double ukupniTrosak = servis.UkupniTrosak(korisnik, odDatuma, doDatuma, new List<KategorijaTroska> { KategorijaTroska.Rezije, KategorijaTroska.Hrana });
Console.WriteLine($"Ukupni trošak: {ukupniTrosak}");

double varijansaTroskova = servis.VarijansaTroskova(korisnik, odDatuma, doDatuma);
Console.WriteLine($"Varijansa troškova: {varijansaTroskova}");*/

