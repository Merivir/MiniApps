using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniWeatherUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void GetWeather_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();
            string apiKey = "a1574b1e2c28919231c72968cf201ef6";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";

            try
            {
                using var client = new HttpClient();
                string json = await client.GetStringAsync(url);

                using JsonDocument doc = JsonDocument.Parse(json);
                double temp = doc.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
                string weather = doc.RootElement.GetProperty("weather")[0].GetProperty("main").GetString();

                ResultTextBlock.Text = $"🌡️ {temp}°C, ☁️ {weather}";
            }
            catch
            {
                ResultTextBlock.Text = "❌ Cannot load weather.";
            }
        }

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CityTextBox.Text == "Enter your city (e.g. Yerevan)")
            {
                CityTextBox.Text = "";
                CityTextBox.Foreground = Brushes.Black;
            }
        }

        private void CityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                CityTextBox.Text = "Enter your city (e.g. Yerevan)";
                CityTextBox.Foreground = Brushes.Gray;
            }
        }
    }
}