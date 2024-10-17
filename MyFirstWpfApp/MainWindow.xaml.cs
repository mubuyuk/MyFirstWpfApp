using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MyFirstWpfApp.Model;


namespace MyFirstWpfApp
{
    public partial class MainWindow : Window
    {
        private Bokningshantering bokningshantering = new Bokningshantering();
        private Användare anvandare = new Användare("Murat ");

        public MainWindow()
        {
            InitializeComponent();
            ListaPåPass();
            UppdateraListView(bokningshantering.GetAllaPass()); // Visa alla pass vid start
        

            FiltreraComboBox.ItemsSource = new List<string>() {"Namn", "Kategori", "Tid" };

            //FiltreraComboBox.ItemsSource = typeof(Pass).GetProperties().Select((o) => o.Name);

            
        }

        // Metod för att lägga till pass
        public void ListaPåPass()
        {
            bokningshantering.LäggTillPass(new Pass("Yoga", "Flexibilitet", "8:00", 20));
            bokningshantering.LäggTillPass(new Pass("Spinning", "Kondition", "9:00", 15));
            bokningshantering.LäggTillPass(new Pass("Crossfit", "Styrka", "10:00", 10));
            bokningshantering.LäggTillPass(new Pass("Padel", "Kondition", "11:00", 4));
            bokningshantering.LäggTillPass(new Pass("Pilates", "Flexibilitet", "12:00", 35));

            
        }

        // Uppdatera ListView med en lista över pass
        private void UppdateraListView(List<Pass> Pass)
        {
            ListViewVisaPass.ItemsSource = null;
            ListViewVisaPass.ItemsSource = Pass; // Sätt ny källa för ListView

        }

        // Metod för att boka ett pass
        private void BokaKnapp_Click(object sender, RoutedEventArgs e)
        {
            Pass valtPass = (Pass)ListViewVisaPass.SelectedItem;

            if (valtPass != null)
            {
                if (bokningshantering.BokaPass(valtPass))
                {
                    MessageBox.Show("Bokning lyckades!");
                    UppdateraListView(bokningshantering.GetAllaPass()); // Uppdatera efter bokning
                }
                else
                {
                    MessageBox.Show("Passet är fullbokat.");
                }
            }
            else
            {
                MessageBox.Show("Välj ett pass att boka.");
            }
        }

        private void AvbokaKnapp_Click(object sender, RoutedEventArgs e)
        {
            Pass valtPass = (Pass)ListViewVisaPass.SelectedItem;

            if (valtPass != null)
            {
                bokningshantering.AvbokaPass(valtPass);
                MessageBox.Show("Bokning avbokad.");
                UppdateraListView(bokningshantering.GetAllaPass()); // Uppdatera efter avbokning
            }
            else
            {
                MessageBox.Show("Välj ett pass att avboka.");
            }
        }








        //private void BokaKnapp_Click(object sender, RoutedEventArgs e)
        //{
        //    anvandare.Boka();  // Anropar användarens bokningsmetod
        //}

        //private void AvbokaKnapp_Click(object sender, RoutedEventArgs e)
        //{
        //    anvandare.AvBoka(); // Anropar användarens avbokningsmetod
        //}

    }
}

