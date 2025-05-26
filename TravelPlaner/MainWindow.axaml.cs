using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TravelPlaner;

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

}