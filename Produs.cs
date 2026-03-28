using System;

namespace MagazinParis
{
    public enum CategorieProdus
    {
        Necunoscut,
        Patiserie,
        Bauturi,
        Dulciuri,
        Gustari
    }

    [Flags]
    public enum CaracteristiciProdus
    {
        Niciuna = 0,
        Bio = 1,
        Vegan = 2,
        FaraZahar = 4,
        FaraGluten = 8
    }

    public class Produs
    {
        public string CodUnic;
        public string Nume;
        public double Pret;
        public int Cantitate;
        public CategorieProdus Categorie;
        public CaracteristiciProdus Caracteristici;

        public Produs(string codUnic, string nume, double pret, int cantitate, CategorieProdus categorie, CaracteristiciProdus caracteristici)
        {
            CodUnic = codUnic;
            Nume = nume;
            Pret = pret;
            Cantitate = cantitate;
            Categorie = categorie;
            Caracteristici = caracteristici;
        }

        public void AfisareInfo()
        {
            Console.WriteLine($"[{CodUnic}] {Nume} | Categorie: {Categorie} | Info: {Caracteristici} | Pret: {Pret} RON | Stoc: {Cantitate} buc.");
        }
    }
}