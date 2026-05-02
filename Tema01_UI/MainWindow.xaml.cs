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

        public MainWindow()
        {
            InitializeComponent();

            adminProduse = new AdministrareProduse_FisierText("Produse.txt");
            inventar = new Inventar(100);
            ProduseAfisate = new ObservableCollection<Produs>();

            adminProduse.CitesteDinFisierInInventar(inventar);
            IncarcaProduseInLista();

            // Configurare controale Lab 9
            lstCategorii.ItemsSource = Enum.GetValues(typeof(CategorieProdus));
            cmbSelectieProdus.ItemsSource = ProduseAfisate;
            dgProduse.ItemsSource = ProduseAfisate;
            dpDataAdaugarii.SelectedDate = DateTime.Now;
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
            try
            {
                Produs p = PreiaDateDinFormular();
                inventar.AdaugaProdus(p);
                ProduseAfisate.Add(p);
                ActualizeazaSelectie();
                MessageBox.Show("Produs adăugat!");
                CurataCampuri();
            }
            catch (Exception ex) { MessageBox.Show("Eroare: " + ex.Message); }
        }

        // Cerința 2: Operația de Modificare
        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            Produs selectat = cmbSelectieProdus.SelectedItem as Produs;
            if (selectat == null)
            {
                MessageBox.Show("Te rog selectează un produs din listă pentru a-l modifica!");
                return;
            }

            try
            {
                // Actualizăm obiectul existent în inventar
                selectat.CodUnic = txtCod.Text;
                selectat.Nume = txtNume.Text;
                selectat.Pret = double.Parse(txtPret.Text);
                selectat.Cantitate = int.Parse(txtCantitate.Text);
                selectat.DataAdaugarii = dpDataAdaugarii.SelectedDate ?? DateTime.Now;
                
                if (lstCategorii.SelectedItem != null)
                    selectat.Categorie = (CategorieProdus)lstCategorii.SelectedItem;

                // Caracteristici (Flags)
                selectat.Caracteristici = CaracteristiciProdus.Niciuna;
                foreach (CheckBox cb in pnlCaracteristici.Children.OfType<CheckBox>())
                    if (cb.IsChecked == true)
                        selectat.Caracteristici |= (CaracteristiciProdus)Enum.Parse(typeof(CaracteristiciProdus), cb.Tag.ToString());

                // Forțăm reîmprospătarea UI (Cerința Lab 9)
                dgProduse.Items.Refresh();
                ActualizeazaSelectie();
                
                MessageBox.Show("Produsul a fost modificat cu succes!");
            }
            catch (Exception ex) { MessageBox.Show("Eroare la modificare: " + ex.Message); }
        }

        private void cmbSelectieProdus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Produs selectat = cmbSelectieProdus.SelectedItem as Produs;
            if (selectat != null)
            {
                txtCod.Text = selectat.CodUnic;
                txtNume.Text = selectat.Nume;
                txtPret.Text = selectat.Pret.ToString();
                txtCantitate.Text = selectat.Cantitate.ToString();
                dpDataAdaugarii.SelectedDate = selectat.DataAdaugarii;
                lstCategorii.SelectedItem = selectat.Categorie;

                // Resetare CheckBoxes
                foreach (CheckBox cb in pnlCaracteristici.Children.OfType<CheckBox>())
                {
                    CaracteristiciProdus val = (CaracteristiciProdus)Enum.Parse(typeof(CaracteristiciProdus), cb.Tag.ToString());
                    cb.IsChecked = selectat.Caracteristici.HasFlag(val);
                }
            }
        }

        private Produs PreiaDateDinFormular()
        {
            CategorieProdus cat = lstCategorii.SelectedItem != null ? (CategorieProdus)lstCategorii.SelectedItem : CategorieProdus.Necunoscut;
            
            CaracteristiciProdus car = CaracteristiciProdus.Niciuna;
            foreach (CheckBox cb in pnlCaracteristici.Children.OfType<CheckBox>())
                if (cb.IsChecked == true)
                    car |= (CaracteristiciProdus)Enum.Parse(typeof(CaracteristiciProdus), cb.Tag.ToString());

            Produs p = new Produs(txtCod.Text, txtNume.Text, double.Parse(txtPret.Text), int.Parse(txtCantitate.Text), cat, car);
            p.DataAdaugarii = dpDataAdaugarii.SelectedDate ?? DateTime.Now;
            return p;
        }

        private void ActualizeazaSelectie()
        {
            // Forțăm reîmprospătarea ComboBox-ului de selecție
            var temp = cmbSelectieProdus.ItemsSource;
            cmbSelectieProdus.ItemsSource = null;
            cmbSelectieProdus.ItemsSource = temp;
        }

        private void txtCautare_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtCautare.Text.ToLower();
            dgProduse.ItemsSource = ProduseAfisate.Where(p => p.Nume.ToLower().Contains(search) || p.CodUnic.ToLower().Contains(search)).ToList();
        }

        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            adminProduse.SalveazaInventarInFisier(inventar);
            MessageBox.Show("Date salvate!");
        }

        private void btnIesire_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void CurataCampuri()
        {
            txtCod.Clear(); txtNume.Clear(); txtPret.Clear(); txtCantitate.Clear();
            dpDataAdaugarii.SelectedDate = DateTime.Now;
            lstCategorii.SelectedIndex = -1;
            foreach (CheckBox cb in pnlCaracteristici.Children.OfType<CheckBox>()) cb.IsChecked = false;
        }
    }
}
