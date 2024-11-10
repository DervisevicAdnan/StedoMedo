// See https://aka.ms/new-console-template for more information
using StedoMedo.Data;
using StedoMedo.Models;
using StedoMedo.Services;

Console.WriteLine("BudzetService................");
Console.WriteLine("Testing...................");
Console.WriteLine("20%....................");
Console.WriteLine("46%.......................");
Console.WriteLine("78%.....................");
Console.WriteLine("99%....................");
Console.WriteLine("Mission completed........");
try
{
    var db = new DbClass();
    var budzetService = new BudzetService(db);

    Korisnik korisnik = new Korisnik(1, "Adi", "Drakovac", "adrakovac2@etf.unsa.ba", "adrakovac", "hashLozinka", "062-009-537");
    db.AddKorisnik(korisnik);
    
    budzetService.DodajBudzet(korisnik, new DateOnly(2024, 11, 1), new DateOnly(2024, 12, 1), 5000);
    Trosak trosak = new Trosak(1, korisnik, DateTime.Parse("2024-11-10"), 2500, KategorijaTroska.Hrana, "Kupovina hrane za nedelju");
    Trosak trosak2 = new Trosak(2, korisnik, DateTime.Parse("2024-11-02"), 2000, KategorijaTroska.Prijevoz, "Kupovina karte za prevoz");
    Trosak trosak3 = new Trosak(3, korisnik, DateTime.Parse("2024-12-01"), 250, KategorijaTroska.Rezije, "Placanje rezija");
    db.AddTrosak(trosak);
    db.AddTrosak(trosak2);
    db.AddTrosak(trosak3);

    double preostaloStanje = budzetService.PreostaloStanjeBudzeta(korisnik, new DateOnly(2024, 12, 1));

    Console.WriteLine($"Preostalo stanje budžeta: {preostaloStanje}");
}
catch(Exception e){
    Console.WriteLine(e.Message);
}