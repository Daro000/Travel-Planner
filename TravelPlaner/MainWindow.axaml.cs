using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;


namespace TravelPlaner
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CityBox.Text))
            {
                CityList.Items.Add(CityBox.Text.Trim());
                CityBox.Text = string.Empty;
            }
        }

        private void ShowSummary_Click(object sender, RoutedEventArgs e)
        {
            
            Console.WriteLine(NameBox == null ? "NameBox jest null" : "NameBox OK");
            Console.WriteLine(CountryComboBox == null ? "CountryComboBox jest null" : "CountryComboBox OK");
            Console.WriteLine(StartDatePicker == null ? "StartDatePicker jest null" : "StartDatePicker OK");
            Console.WriteLine(EndDatePicker == null ? "EndDatePicker jest null" : "EndDatePicker OK");
            Console.WriteLine(CountryImage == null ? "CountryImage jest null" : "CountryImage OK");
            
            if (CountryComboBox.SelectedItem is not ComboBoxItem selectedItem || selectedItem.Content == null)
            {
                Console.WriteLine("Proszę wybrać kraj.");
                return;
            }
            
            var trip = new TripData
            {
                FullName = NameBox.Text,
                
                Country = selectedItem.Content.ToString(),
                
                StartDate = StartDatePicker.SelectedDate != null
                    ? new DateTime(StartDatePicker.SelectedDate.Value.Year, StartDatePicker.SelectedDate.Value.Month, StartDatePicker.SelectedDate.Value.Day)
                    : DateTime.Today,
                
                EndDate = EndDatePicker.SelectedDate != null
                    ? new DateTime(EndDatePicker.SelectedDate.Value.Year, EndDatePicker.SelectedDate.Value.Month, EndDatePicker.SelectedDate.Value.Day)
                    : DateTime.Today.AddDays(7),
                
                ImagePath = CountryImage.Source.ToString()
            };

            if (MuseumCheck.IsChecked == true) trip.Attractions.Add("Muzea");
            if (ParksCheck.IsChecked == true) trip.Attractions.Add("Parki Narodowe");
            if (MonumentsCheck.IsChecked == true) trip.Attractions.Add("Zabytki");
            if (RestaurantsCheck.IsChecked == true) trip.Attractions.Add("Restauracje");
            if (GalleriesCheck.IsChecked == true) trip.Attractions.Add("Galerie sztuki");
            if (FestivalsCheck.IsChecked == true) trip.Attractions.Add("Festiwale i koncerty");

            trip.Transport = PlaneRadio.IsChecked == true ? "Samolot" :
                CarRadio.IsChecked == true ? "Samochód" :
                TrainRadio.IsChecked == true ? "Pociąg" :
                ShipRadio.IsChecked == true ? "Statek" : "";

            foreach (var item in CityList.Items)
                trip.Cities.Add(item.ToString());

            var summaryWindow = new SummaryWindow(trip);
            summaryWindow.Show();
        }

        private void CountryComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string country = selectedItem.Content?.ToString()?.ToLower() ?? "";
                string imagePath = $"Images/{country}.jpg"; // np. Images/japonia.jpg

                if (File.Exists(imagePath))
                {
                    CountryImage.Source = new Bitmap(imagePath);
                }
            }
        }
    }
    
}

