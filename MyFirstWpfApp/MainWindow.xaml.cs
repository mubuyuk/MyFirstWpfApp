using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MyFirstWpfApp.Model;


namespace MyFirstWpfApp
{
    public partial class MainWindow : Window
    {   // Instans av Bokningshantering som innehåller alla pass och bokningar
        private Bokningshantering bokningshantering = new Bokningshantering();

        // En instans av Användare för att simulera en inloggad användare
        Användare AktivAnvändare = new Användare("Murat");

        public MainWindow()
        {
            InitializeComponent();
            ListaPåPass();  // Lägger till initiala pass till listan
            UppdateraListView(bokningshantering.GetAllaPass()); // Visa alla pass vid start

            // Sätter upp filtreringsalternativ för ComboBoxen
            FiltreraComboBox.ItemsSource = new List<string>() { "Namn", "Kategori", "Tid" };

            // Användare för att simulera en inloggad användare i UI
            InloggadAnvändareTextBlock.Text = $"Inloggad: {AktivAnvändare.Namn}";

        }

        // Metod för att lägga till pass till bokningshanteringen
        public void ListaPåPass()
        {
            // Skapa en lista över pass med information om namn, kategori, tid och antal platser
            var passLista = new List<Pass>
            {
                new Pass("Yoga", "Flexibilitet", "8:00", 20),
                new Pass("Spinning", "Kondition", "9:00", 15),
                new Pass("Crossfit", "Styrka", "9:00", 10),
                new Pass("Padel", "Kondition", "11:00", 4, 4),
                new Pass("Pilates", "Flexibilitet", "12:00", 35)
            };

            // Lägg till varje pass i bokningshanteringen
            passLista.ForEach(p => bokningshantering.LäggTillPass(p));
        }


        public Predicate<object> GetFilter()
        {
            return obj =>
            {
                var passObj = obj as Pass;
                if (passObj == null) return false;

                string sökText = SökTextBox.Text ?? string.Empty;

                return FiltreraComboBox.SelectedItem switch
                {
                    "Namn" => passObj.Namn.Contains(sökText, StringComparison.OrdinalIgnoreCase),
                    "Kategori" => passObj.Kategori.Contains(sökText, StringComparison.OrdinalIgnoreCase),
                    "Tid" => passObj.Tid.Contains(sökText, StringComparison.OrdinalIgnoreCase),
                    _ => false
                };
            };
        }

        //// Filtreringsfunktion som kontrollerar om Namnet innehåller söktexten
        //private bool NamnFilter(object obj)
        //{
        //    var Filterobj = obj as Pass;

        //    return Filterobj.Namn.Contains(SökTextBox.Text,StringComparison.OrdinalIgnoreCase);

        //}
        //// Filtreringsfunktion som kontrollerar om Kategorin innehåller söktexten
        //private bool KategoriFilter(object obj)
        //{
        //    var Filterobj = obj as Pass;

        //    return Filterobj.Kategori.Contains(SökTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}
        //// Filtreringsfunktion som kontrollerar om Tiden innehåller söktexten
        //private bool TidFilter(object obj)
        //{
        //    var Filterobj = obj as Pass;

        //    return Filterobj.Tid.Contains(SökTextBox.Text, StringComparison.OrdinalIgnoreCase);

        //}

        // Metod som triggas när texten i sökfältet ändras
        private void SökTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Sätt filtrering baserat på om söktexten är tom eller inte
            ListViewVisaPass.Items.Filter = string.IsNullOrEmpty(SökTextBox.Text) ? null : GetFilter();
        }

        // Metod som triggas när användaren ändrar val i ComboBoxen
        private void FiltreraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Använd det valda filtret baserat på användarens val
            ListViewVisaPass.Items.Filter = GetFilter();
        }


        // Uppdatera ListView med en lista över pass
        private void UppdateraListView(List<Pass> Pass)
        {
            ListViewVisaPass.ItemsSource = null; // Nollställer källan för att tvinga uppdatering
            ListViewVisaPass.ItemsSource = Pass; // Sätt ny källa för ListView
        }

        // Metod för att boka ett pass
        private void BokaKnapp_Click(object sender, RoutedEventArgs e)
        {
            // Hämtar det pass som är valt i ListView
            Pass valtPass = (Pass)ListViewVisaPass.SelectedItem;


            if (valtPass != null) //Kontrollera att ett pass är valt
            {
                // Kontrollera om bokningen lyckas
                if (bokningshantering.BokaPass(AktivAnvändare, valtPass))
                {
                    MessageBox.Show("Bokning lyckades!", "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
                    UppdateraListView(bokningshantering.GetAllaPass()); // Uppdatera efter bokning
                }
                else
                {
                    // Kontrollera om passet är fullbokat eller om användaren redan har bokat
                    if (valtPass.ÄrFullbokat)
                    {
                        MessageBox.Show("Passet är fullbokat.", "Meddelande",MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else 
                    {
                        MessageBox.Show("Du har redan bokat detta pass.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Välj ett pass att boka!", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        // Metod för att avboka pass
        private void AvbokaKnapp_Click(object sender, RoutedEventArgs e)
        {
            Pass valtPass = (Pass)ListViewVisaPass.SelectedItem;

            if (valtPass != null)
            {
                bokningshantering.AvbokaPass(AktivAnvändare, valtPass);
                MessageBox.Show("Din bokning är avbokad.", "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                UppdateraListView(bokningshantering.GetAllaPass()); // Uppdatera efter avbokning
            }
            else
            {
                MessageBox.Show("Välj ett pass att avboka!", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}

