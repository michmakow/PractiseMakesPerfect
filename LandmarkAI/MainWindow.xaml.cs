using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Imgae files (*.png; *.jpg)|*.png;*.jpg;*jpeg| All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;
                SelectedImage.Source = new BitmapImage(new Uri(fileName));

                MakePredictionAsync(fileName);
            }
        }

        private async void MakePredictionAsync(string fileName)
        {
            var url =
                "https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/640571d2-e30b-42b7-ba45-87b7a09c6c72/classify/iterations/Test/image";
            var predictionKey = "0df6ffecf8f4473b96231efaa81dad2d";
            var contentType = "application/octet-stream";
            var file = File.ReadAllBytes(fileName);

            using var httpClient = new HttpClient();
            using var content = new ByteArrayContent(file);

            httpClient.DefaultRequestHeaders.Add("Prediction-Key",predictionKey);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            
            var response = await httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            var predictions = JsonConvert.DeserializeObject<CustomVision>(responseString).Predictions;
            
        }
    }
}
