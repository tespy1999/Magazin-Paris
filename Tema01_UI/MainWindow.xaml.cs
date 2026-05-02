using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MagazinParis
{
    public partial class MainWindow : Window
    {
        private AdministrareProduse_FisierText adminProduse;
        private Inventar inventar;
        public ObservableCollection<Produs> ProduseAfisate { get; set; }

        private const int LUNGIME_MIN_COD = 3;
        private const int LUNGIME_MAX_NUME = 50;
        private const double PRET_MINIM = 0.01;

        public MainWindow()
        {
            InitializeComponent();

            // Inițializare Backend
            adminProduse = new AdministrareProduse_FisierText("Produse.txt");
            inventar = new Inventar(100);
            ProduseAfisate = new ObservableCollection<Produs>();

            // Încărcare date
            adminProduse.CitesteDinFisierInInventar(inventar);
            IncarcaProduseInLista();

            dgProduse.ItemsSource = ProduseAfisate;
        }

        private void IncarcaProduseInLista()
        {
            ProduseAfisate.Clear();
            for (int i = 0; i < inventar.numarProduse; i++)
            {
                ProduseAfisate.Add(inventar.produse[i]);
            }
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidareDate()) return;

            try
            {
                // Preluare Categorie din RadioButtons (Cerința 3)
                CategorieProdus categorie = CategorieProdus.Necunoscut;
                foreach (RadioButton rb in pnlCategorii.Children.OfType<RadioButton>())
                {
                    if (rb.IsChecked == true)
                    {
                        categorie = (CategorieProdus)Enum.Parse(typeof(CategorieProdus), rb.Tag.ToString());
                        break;
                    }
                }

                // Preluare Caracteristici din CheckBoxes (Cerința 3)
                CaracteristiciProdus caracteristici = CaracteristiciProdus.Niciuna;
                foreach (CheckBox cb in pnlCaracteristici.Children.OfType<CheckBox>())
                {
                    if (cb.IsChecked == true)
                    {
                        CaracteristiciProdus feature = (CaracteristiciProdus)Enum.Parse(typeof(CaracteristiciProdus), cb.Tag.ToString());
                        caracteristici |= feature; // Bitwise OR pentru Flags
                    }
                }

                Produs p = new Produs(
                    txtCod.Text, 
                    txtNume.Text, 
                    double.Parse(txtPret.Text), 
                    int.Parse(txtCantitate.Text), 
                    categorie, 
                    caracteristici
                );

                inventar.AdaugaProdus(p);
                ProduseAfisate.Add(p);
                
                MessageBox.Show("Produs adăugat cu succes!");
                CurataCampuri();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        private bool ValidareDate()
        {
            bool ok = true;
            // Resetare culori
            lblCod.Foreground = Brushes.Black;
            lblNume.Foreground = Brushes.Black;

            if (txtCod.Text.Length < LUNGIME_MIN_COD) { lblCod.Foreground = Brushes.Red; ok = false; }
            if (string.IsNullOrWhiteSpace(txtNume.Text)) { lblNume.Foreground = Brushes.Red; ok = false; }
            
            return ok;
        }

        // Funcționalitate de Căutare (Cerința 4)
        private void txtCautare_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textCautat = txtCautare.Text.ToLower();
            ProduseAfisate.Clear();

            for (int i = 0; i < inventar.numarProduse; i++)
            {
                if (inventar.produse[i].Nume.ToLower().Contains(textCautat) || 
                    inventar.produse[i].CodUnic.ToLower().Contains(textCautat))
                {
                    ProduseAfisate.Add(inventar.produse[i]);
                }
            }
        }

        // Gestionare Meniu
        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            adminProduse.SalveazaInventarInFisier(inventar);
            MessageBox.Show("Date salvate în Produse.txt");
        }

        private void btnIesire_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnDespre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicație Gestiune Magazin Paris\nLaborator 8 WPF\nRealizat de: Tarytsa Adrian", "Despre");
        }

        private void CurataCampuri()
        {
            txtCod.Clear();
            txtNume.Clear();
            txtPret.Clear();
            txtCantitate.Clear();
        }

        private void btnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            CurataCampuri();
        }
    }
}
