using System;

namespace MagazinParis
{
    public enum CategorieProdus
    {
        Necunoscut = 0,
        Patiserie = 1,
        Bauturi = 2,
        Dulciuri = 3,
        Gustari = 4
    }

    [Flags]
    public enum CaracteristiciProdus
    {
        Niciuna = 0,
        Bio = 1,
        Vegan = 2,
        FaraZahar = 3,
        FaraGluten = 4
    }

    public class Produs
    {
        public string CodUnic { get; set; }
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Cantitate { get; set; }
        public CategorieProdus Categorie { get; set; }
        public CaracteristiciProdus Caracteristici { get; set; }
        public DateTime DataAdaugarii { get; set; }

        public Produs() 
        {
            DataAdaugarii = DateTime.Now;
        }

        public Produs(string codUnic, string nume, double pret, int cantitate, CategorieProdus categorie, CaracteristiciProdus caracteristici)
        {
            CodUnic = codUnic;
            Nume = nume;
            Pret = pret;
            Cantitate = cantitate;
            Categorie = categorie;
            Caracteristici = caracteristici;
            DataAdaugarii = DateTime.Now;
        }

        public Produs(string linieFisier)
        {
            string[] date = linieFisier.Split(';');
            CodUnic = date[0].Trim();
            Nume = date[1].Trim();
            Pret = double.Parse(date[2].Trim());
            Cantitate = int.Parse(date[3].Trim());
            Categorie = (CategorieProdus)Enum.Parse(typeof(CategorieProdus), date[4].Trim(), true);
            Caracteristici = (CaracteristiciProdus)Enum.Parse(typeof(CaracteristiciProdus), date[5].Trim(), true);
            
            if (date.Length > 6)
                DataAdaugarii = DateTime.Parse(date[6].Trim());
            else
                DataAdaugarii = DateTime.Now;
        }

        public string ConversieLaSir_PentruFisier()
        {
            return $"{CodUnic};{Nume};{Pret};{Cantitate};{Categorie};{Caracteristici};{DataAdaugarii:yyyy-MM-dd}";
        }

        public void AfisareInfo()
        {
            Console.WriteLine($"[{CodUnic}] {Nume} | Categorie: {Categorie} | Info: {Caracteristici} | Pret: {Pret} RON | Stoc: {Cantitate} buc.");
        }
    }
}