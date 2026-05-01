using System.Windows;

namespace MagazinParis
{
    public partial class MainWindow : Window
    {
        public Produs ProdusDeAfisat { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ProdusDeAfisat = new Produs(
                "P001", 
                "Ecler cu ciocolata", 
                8.50, 
                15, 
                CategorieProdus.Dulciuri, 
                CaracteristiciProdus.Niciuna
            );

            this.DataContext = ProdusDeAfisat;
        }
    }
}
