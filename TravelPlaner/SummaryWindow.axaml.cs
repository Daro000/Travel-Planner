using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace TravelPlaner;

public partial class SummaryWindow : Window
{
    public SummaryWindow(TripData trip)
    {
        InitializeComponent();

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

    private async void SaveToFile_Click(object? sender, RoutedEventArgs e)
    {
        var sfd = new SaveFileDialog
        {
            InitialFileName = "plan_podrozy.txt",
            Filters = { new FileDialogFilter { Name = "Pliki tekstowe", Extensions = { "txt" } } }
        };

        var path = await sfd.ShowAsync(this);
        if (!string.IsNullOrWhiteSpace(path))
        {
            File.WriteAllText(path, SummaryText.Text);
        }
    }
}