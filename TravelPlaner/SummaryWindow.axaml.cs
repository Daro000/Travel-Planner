using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;

namespace TravelPlaner
{
    public partial class SummaryWindow : Window
    {
        private TripData _trip;

        public SummaryWindow(TripData trip)
        {
            InitializeComponent();

            _trip = trip;

            SummaryText.Text = $"Podróżujący: {trip.FullName}\n" +
                               $"Kraj: {trip.Country}\n" +
                               $"Atrakcje: {string.Join(", ", trip.Attractions)}\n" +
                               $"Transport: {trip.Transport}\n" +
                               $"Miasta: {string.Join(", ", trip.Cities)}\n" +
                               $"Termin: {trip.StartDate:dd.MM.yyyy} - {trip.EndDate:dd.MM.yyyy}\n" +
                               $"Szacowany koszt: {trip.EstimateCost():C}";

            if (!string.IsNullOrEmpty(trip.ImagePath) && File.Exists(trip.ImagePath))
            {
                SummaryImage.Source = new Bitmap(trip.ImagePath);
            }
        }

        private void SaveToFile_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText("plan_podrozy.txt", SummaryText.Text);
                Console.WriteLine("Zapisano plik");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd zapisu pliku: " + ex.Message);
            }
        }
    }
}