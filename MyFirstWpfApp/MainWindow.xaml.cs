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

            //ListViewVisaPass.Items.Filter = NamnFilter;
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

        public Predicate<object> GetFilter() 
        { 
            switch (FiltreraComboBox.SelectedItem as string) 
            {
                case "Namn":
                    return NamnFilter;

                case "Kategori":
                    return KategoriFilter;

                case "Tid":
                    return TidFilter;
            }

            return NamnFilter;
        }
        

        private bool NamnFilter(object obj)
        {
            var Filterobj = obj as Pass;

            return Filterobj.Namn.Contains(SökTextBox.Text,StringComparison.OrdinalIgnoreCase);

        }

        private bool KategoriFilter(object obj)
        {
            var Filterobj = obj as Pass;

            return Filterobj.Kategori.Contains(SökTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private bool TidFilter(object obj)
        {
            var Filterobj = obj as Pass;

            return Filterobj.Tid.Contains(SökTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }

        private void SökTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SökTextBox.Text == null)
            {
                ListViewVisaPass.Items.Filter = null;
            }

            else
            {
                ListViewVisaPass.Items.Filter = GetFilter();
            }
        }

        private void FiltreraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewVisaPass.Items.Filter = GetFilter();

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

       
    }
}

