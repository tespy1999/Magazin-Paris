using System;

namespace MagazinParis
{
    public class Inventar
    {
        public Produs[] produse;
        public int numarProduse;

        public Inventar(int capacitateMaxima)
        {
            produse = new Produs[capacitateMaxima];
            numarProduse = 0;
        }

        public void AdaugaProdus(Produs produsNou)
        {
            if (numarProduse < produse.Length)
            {
                produse[numarProduse] = produsNou;
                numarProduse++;
            }
            else
            {
                Console.WriteLine("Inventarul este plin!");
            }
        }

        public void AfiseazaToate()
        {
            for (int i = 0; i < numarProduse; i++)
            {
                produse[i].AfisareInfo();
            }
        }

        public Produs CautaDupaCod(string codCautat)
        {
            for (int i = 0; i < numarProduse; i++)
            {
                if (produse[i].CodUnic == codCautat)
                {
                    return produse[i];
                }
            }
            return null;
        }
    }
}