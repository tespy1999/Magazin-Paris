using System;

namespace MagazinParis
{
    class Program
    {
        static void Main()
        {
            AdministrareProduse_FisierText adminProduse = new AdministrareProduse_FisierText("Produse.txt");
            AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText("Clienti.txt");

            Console.WriteLine("ADMINISTRARE MAGAZIN PARIS\n");

            Console.Write("Cate produse vrei sa adaugi in magazin? ");
            int nrProduse;
            while (!int.TryParse(Console.ReadLine(), out nrProduse))
            {
                Console.Write("Ai tastat gresit! Trebuie sa introduci un numar: ");
            }

            Inventar inventarMagazin = new Inventar(nrProduse + 50);

            adminProduse.CitesteDinFisierInInventar(inventarMagazin);

            for (int i = 0; i < nrProduse; i++)
            {
                Console.WriteLine($"\nIntroducere date pentru produsul {i + 1}");

                Console.Write("Cod Unic: ");
                string cod = Console.ReadLine();

                Console.Write("Nume produs: ");
                string nume = Console.ReadLine();

                Console.Write("Pret: ");
                double pret;
                while (!double.TryParse(Console.ReadLine(), out pret))
                {
                    Console.Write("Eroare! Pretul trebuie sa fie un numar: ");
                }

                Console.Write("Cantitate (stoc): ");
                int cantitate;
                while (!int.TryParse(Console.ReadLine(), out cantitate))
                {
                    Console.Write("Cantitatea trebuie sa fie un numar intreg: ");
                }

                Console.WriteLine("Categorii: 0-Necunoscut, 1-Patiserie, 2-Bauturi, 3-Dulciuri, 4-Gustari");
                Console.Write("Introdu ID-ul categoriei: ");
                int idCategorie;
                while (!int.TryParse(Console.ReadLine(), out idCategorie))
                {
                    Console.Write("Introdu o cifra de la 0 la 4: ");
                }
                CategorieProdus categorie = (CategorieProdus)idCategorie;

                Console.WriteLine("Caracteristici: 0-Niciuna, 1-Bio, 2-Vegan, 3-FaraZahar, 4-FaraGluten");
                Console.Write("Introdu ID-ul caracteristicii: ");
                int idCaracteristici;
                while (!int.TryParse(Console.ReadLine(), out idCaracteristici))
                {
                    Console.Write("Ai gresit! Trebuie sa introduci un numar valid: ");
                }
                CaracteristiciProdus caracteristici = (CaracteristiciProdus)idCaracteristici;

                Produs produsNou = new Produs(cod, nume, pret, cantitate, categorie, caracteristici);
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
                double bugetClient;
                while (!double.TryParse(Console.ReadLine(), out bugetClient))
                {
                    Console.Write("Buget invalid! Banii se scriu in cifre. Introdu din nou: ");
                }

                Client clientTastatura = new Client(numeClient, bugetClient);

                Console.Write($"Cate bucati de '{produsGasit.Nume}' vrea sa cumpere? ");
                int bucati;
                while (!int.TryParse(Console.ReadLine(), out bucati))
                {
                    Console.Write("Introdu un numar intreg de bucati: ");
                }

                clientTastatura.Cumpara(produsGasit, bucati);

                adminClienti.AdaugaClient(clientTastatura);
            }
            else
            {
                Console.WriteLine($"\nEroare: Produsul cu codul '{codCautat}' nu a fost gasit in magazin.");
            }

            adminProduse.SalveazaInventarInFisier(inventarMagazin);
            Console.WriteLine("\nToate datele au fost salvate cu succes in fisiere!");

            Console.ReadKey();
        }
    }
}