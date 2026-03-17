using System;

namespace MagazinParis
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ADMINISTRARE MAGAZIN PARIS\n");

            Console.Write("Cate produse vrei sa adaugi in magazin? ");
            int nrProduse = int.Parse(Console.ReadLine());

            Inventar inventarMagazin = new Inventar(nrProduse);

            for (int i = 0; i < nrProduse; i++)
            {
                Console.WriteLine($"\nIntroducere date pentru produsul {i + 1}");

                Console.Write("Cod Unic: ");
                string cod = Console.ReadLine();

                Console.Write("Nume produs: ");
                string nume = Console.ReadLine();

                Console.Write("Pret: ");
                double pret = double.Parse(Console.ReadLine());

                Console.Write("Cantitate (stoc): ");
                int cantitate = int.Parse(Console.ReadLine());

                Produs produsNou = new Produs(cod, nume, pret, cantitate);
                inventarMagazin.AdaugaProdus(produsNou);
            }

            Console.WriteLine("\nINVENTARUL MAGAZINULUI");
            inventarMagazin.AfiseazaToate();

            Console.WriteLine("\nCAUTARE SI VANZARE PRODUS");
            Console.Write("Introdu codul produsului pe care vrei sa il cauti: ");
            string codCautat = Console.ReadLine();

            Produs produsGasit = inventarMagazin.CautaDupaCod(codCautat);

            if (produsGasit != null)
            {
                Console.WriteLine("\n[PRODUS GASIT]");
                produsGasit.AfisareInfo();

                Console.WriteLine("\nClient nou la casa");
                Console.Write("Nume client: ");
                string numeClient = Console.ReadLine();

                Console.Write("Buget client (RON): ");
                double bugetClient = double.Parse(Console.ReadLine());

                Client clientTastatura = new Client(numeClient, bugetClient);

                Console.Write($"Cate bucati de '{produsGasit.Nume}' vrea sa cumpere? ");
                int bucati = int.Parse(Console.ReadLine());

                clientTastatura.Cumpara(produsGasit, bucati);
            }
            else
            {
                Console.WriteLine($"\nEroare: Produsul cu codul '{codCautat}' nu a fost gasit in magazin.");
            }

            Console.ReadKey();
        }
    }
}