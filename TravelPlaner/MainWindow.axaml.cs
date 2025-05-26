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
                CityBox.Clear();
            }
        }

        private void ShowSummary_Click(object sender, RoutedEventArgs e)
        {
            var trip = new TripData
            {
                FullName = NameBox.Text,
                Country = (CountryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                
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

