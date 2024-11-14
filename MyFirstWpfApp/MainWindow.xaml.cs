using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MyFirstWpfApp.Model;
using System;

namespace MyFirstWpfApp
{
    public partial class MainWindow : Window
    {
        // Instans av Bokningshantering som innehåller alla pass och bokningar
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
                new Pass("Yoga", "Flexibilitet", DateTime.Today.AddHours(8), 20),
                new Pass("Spinning", "Kondition", DateTime.Today.AddHours(9), 15),
                new Pass("Crossfit", "Styrka", DateTime.Today.AddHours(10), 10),
                new Pass("Padel", "Kondition", DateTime.Today.AddHours(11), 4, 4),
                new Pass("Pilates", "Flexibilitet", DateTime.Today.AddHours(12), 35)
            };

            // Lägg till varje pass i bokningshanteringen
            passLista.ForEach(p => bokningshantering.LäggTillPass(p));
        }

        private void SökOchFiltreraPass()
        {
            var sökText = SökTextBox.Text ?? string.Empty; // Hämta texten från sökfältet
            var valdKategori = FiltreraComboBox.SelectedItem as string;


            var resultat = bokningshantering.GetAllaPass().Where(p =>
            {
                switch (valdKategori)
                {
                    case "Namn":
                        return p.Namn.Contains(sökText, StringComparison.OrdinalIgnoreCase);

                    case "Kategori":
                        return p.Kategori.Contains(sökText, StringComparison.OrdinalIgnoreCase);

                    case "Tid":
                        // Om ingen text anges i sökfältet, visa alla pass
                        if (string.IsNullOrWhiteSpace(sökText)) return true;

                        // Försök att omvandla söktexten till en giltig tid i formatet "HH:mm"
                        if (DateTime.TryParseExact(sökText, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime valdTid))
                        {
                            // Jämför endast tiden i formatet "HH:mm"
                            return p.Tid.ToString("HH:mm") == valdTid.ToString("HH:mm");
                        }
                        else
                        {
                            //MessageBox.Show("Ogiltigt tidsformat. Använd t.ex. '08:00'", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }

                    default:
                        return true; // Om inget specifikt filter är valt, visa alla pass
                }
            }).ToList();

            UppdateraListView(resultat); // Uppdatera ListView med det filtrerade resultatet
        }

        // Filtermetod som används av ListView för att filtrera objekt
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
                    "Tid" => DateTime.TryParseExact(sökText, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime valdTid) &&
                             passObj.Tid.ToString("HH:mm") == valdTid.ToString("HH:mm"),
                    _ => false
                };
            };
        }

        // Metod som triggas när texten i sökfältet ändras
        private void SökTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SökOchFiltreraPass();
        }

        // Metod som triggas när användaren ändrar val i ComboBoxen
        private void FiltreraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SökOchFiltreraPass();
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
                        MessageBox.Show("Passet är fullbokat.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (bokningshantering.HarBokatPass(AktivAnvändare, valtPass))
                {
                    bokningshantering.AvbokaPass(AktivAnvändare, valtPass);
                    MessageBox.Show("Din bokning är avbokad.", "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    UppdateraListView(bokningshantering.GetAllaPass()); // Uppdatera efter avbokning
                }
                else 
                {
                    MessageBox.Show("Du har inte bokat detta pass och kan därför inte avboka det.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Välj ett pass att avboka!", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}