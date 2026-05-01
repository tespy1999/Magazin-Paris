using System;
using System.Windows;
using System.Windows.Media;

namespace MagazinParis
{
    public partial class MainWindow : Window
    {
        // Constante pentru validare conform bunelor practici din Lab 7
        private const int LUNGIME_MIN_COD = 3;
        private const int LUNGIME_MAX_NUME = 50;
        private const double PRET_MINIM = 0.01;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            // Resetează culorile etichetelor și ascunde mesajul de eroare
            ReseteazaStiluri();

            bool esteValid = true;
            string mesajEroare = "";

            // 1. Validare Cod
            if (string.IsNullOrWhiteSpace(txtCod.Text) || txtCod.Text.Length < LUNGIME_MIN_COD)
            {
                lblCod.Foreground = Brushes.Red;
                mesajEroare += $"Codul trebuie să aibă minim {LUNGIME_MIN_COD} caractere!\n";
                esteValid = false;
            }

            // 2. Validare Nume
            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > LUNGIME_MAX_NUME)
            {
                lblNume.Foreground = Brushes.Red;
                mesajEroare += $"Numele este obligatoriu și max {LUNGIME_MAX_NUME} caractere!\n";
                esteValid = false;
            }

            // 3. Validare Preț
            if (!double.TryParse(txtPret.Text, out double pret) || pret < PRET_MINIM)
            {
                lblPret.Foreground = Brushes.Red;
                mesajEroare += "Prețul trebuie să fie un număr pozitiv!\n";
                esteValid = false;
            }

            // 4. Validare Cantitate
            if (!int.TryParse(txtCantitate.Text, out int cantitate) || cantitate < 0)
            {
                lblCantitate.Foreground = Brushes.Red;
                mesajEroare += "Cantitatea trebuie să fie un număr întreg (0 sau mai mult)!\n";
                esteValid = false;
            }

            // Feedback vizual dacă validarea eșuează
            if (!esteValid)
            {
                txtMesajEroare.Text = mesajEroare;
                txtMesajEroare.Visibility = Visibility.Visible;
                return;
            }

            // Dacă validarea trece, creăm obiectul (Backend logic)
            try
            {
                Produs produsNou = new Produs(
                    txtCod.Text, 
                    txtNume.Text, 
                    pret, 
                    cantitate, 
                    CategorieProdus.Necunoscut, 
                    CaracteristiciProdus.Niciuna
                );

                MessageBox.Show($"Produsul '{produsNou.Nume}' a fost validat și adăugat cu succes!", 
                                "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                
                CurataCampuri();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la crearea obiectului: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReseteazaStiluri()
        {
            // Resetăm culoarea etichetelor la cea implicită (DarkSlateGray din resurse sau Black)
            Brush culoareNormala = (Brush)new BrushConverter().ConvertFromString("#2F3640");
            lblCod.Foreground = culoareNormala;
            lblNume.Foreground = culoareNormala;
            lblPret.Foreground = culoareNormala;
            lblCantitate.Foreground = culoareNormala;

            txtMesajEroare.Visibility = Visibility.Collapsed;
        }

        private void CurataCampuri()
        {
            txtCod.Clear();
            txtNume.Clear();
            txtPret.Clear();
            txtCantitate.Clear();
            ReseteazaStiluri();
        }
    }
}
