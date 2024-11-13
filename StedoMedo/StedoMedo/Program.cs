// See https://aka.ms/new-console-template for more information
using StedoMedo.Data;
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
Console.WriteLine(servis.ProcijeniTroskove(test, 1)+" novčanih jedinica");
