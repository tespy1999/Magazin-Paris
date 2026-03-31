using System;
using System.IO;

namespace MagazinParis
{
    public class AdministrareProduse_FisierText
    {
        private string numeFisier;

        public AdministrareProduse_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public void CitesteDinFisierInInventar(Inventar inventarMagazin)
        {
            string[] linii = File.ReadAllLines(numeFisier);

            foreach (string linie in linii)
            {
                if (!string.IsNullOrEmpty(linie))
                {
                    Produs produsIncarcat = new Produs(linie);
                    inventarMagazin.AdaugaProdus(produsIncarcat);
                }
            }
        }

        public void SalveazaInventarInFisier(Inventar inventarMagazin)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                for (int i = 0; i < inventarMagazin.numarProduse; i++)
                {
                    sw.WriteLine(inventarMagazin.produse[i].ConversieLaSir_PentruFisier());
                }
            }
        }
    }
}