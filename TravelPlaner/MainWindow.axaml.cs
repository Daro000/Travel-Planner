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
            
            if (CountryComboBox.SelectedItem is not ComboBoxItem selectedItem || selectedItem.Content == null)
            {
                return;
            }

            var trip = new TripData
            {
                FullName = NameBox.Text,
                Country = selectedItem.Content.ToString(),
                
                StartDate = StartDatePicker.SelectedDate?.DateTime ?? DateTime.Today,
                EndDate = EndDatePicker.SelectedDate?.DateTime ?? DateTime.Today.AddDays(7),
                
                ImagePath = CountryImage.Source?.ToString() ?? string.Empty
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

            foreach (var city in CityList.Items)
                trip.Cities.Add(city?.ToString() ?? "");

            var summaryWindow = new SummaryWindow(trip);
            summaryWindow.Show();
        }

        private void CountryComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string country = selectedItem.Content?.ToString()?.ToLower() ?? "";
                string imagePath = $"RiderProjects\\TravelPlaner\\TravelPlaner\\Images\\{country}.jpg";

                if (File.Exists(imagePath))
                {
                    CountryImage.Source = new Bitmap(imagePath);
                }
            }
        }
    }
    
}

