using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace MyFirstWpfApp
{
    public partial class MainWindow : Window
    {
        private Bokningshantering bokningshantering = new Bokningshantering();
        private Användare anvandare;

        public MainWindow()
        {
            InitializeComponent();
            ListaPåPass();
            UppdateraListView(bokningshantering.GetAllaPass()); // Visa alla pass vid start
            anvandare = new Användare("Murat ");

            FiltreraComboBox.ItemsSource = new List<string>() {"Namn", "Kategori", "Tid" };

            //FiltreraComboBox.ItemsSource = typeof(Pass).GetProperties().Select((o) => o.Name);

        }

        // Metod för att lägga till pass
        public void ListaPåPass()
        {
            bokningshantering.LäggTillPass(new Pass("Yoga", "Flexibilitet", "9:00", 10));
            bokningshantering.LäggTillPass(new Pass("Spinning", "Kondition", "12:00", 10));
            bokningshantering.LäggTillPass(new Pass("Crossfit", "Styrka", "8:00", 10));
            bokningshantering.LäggTillPass(new Pass("Padel", "Kondition", "11:00", 10));
            bokningshantering.LäggTillPass(new Pass("Pilates", "Flexibilitet", "12:00", 10));

            
        }

        // Uppdatera ListView med en lista över pass
        private void UppdateraListView(List<Pass> passLista)
        {
            ListViewVisaPass.ItemsSource = passLista; // Sätt ny källa för ListView

        }

        
        
        
        
        
        
        
        
        
        private void BokaKnapp_Click(object sender, RoutedEventArgs e)
        {
            anvandare.Boka();  // Anropar användarens bokningsmetod
        }

        private void AvbokaKnapp_Click(object sender, RoutedEventArgs e)
        {
            anvandare.AvBoka(); // Anropar användarens avbokningsmetod
        }

    }
}

