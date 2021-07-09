using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Evernote.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var ammountOfCharacters = new TextRange(ContentRichTextBox.Document.ContentStart,
                ContentRichTextBox.Document.ContentEnd).Text.Length;

            StatusTextBlock.Text = $"Documnt lenght: {ammountOfCharacters} characters";
        }



        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var region = "westeurope";
            var key = "b067b92a12c745e39f269980083062b2";

            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = await recognizer.RecognizeOnceAsync();
            ContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
        }

        private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            BoldButton.IsChecked = selectedWeight != DependencyProperty.UnsetValue && selectedWeight.Equals(FontWeights.Bold);

            var selectedStyle = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            ItalicButton.IsChecked = selectedStyle != DependencyProperty.UnsetValue && selectedStyle.Equals(FontStyles.Italic);

            var selectedDecoration = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            UnderlineButton.IsChecked = selectedDecoration != DependencyProperty.UnsetValue && selectedDecoration.Equals(TextDecorations.Underline);
        }

        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            var isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty,
                isButtonEnabled ? FontWeights.Bold : FontWeights.Normal);
        }

        private void ItalicButton_OnClick(object sender, RoutedEventArgs e)
        {
            var isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                isButtonEnabled ? FontStyles.Italic : FontStyles.Normal);
        }

        private void UnderlineButton_OnClick(object sender, RoutedEventArgs e)
        {
            var isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if(isButtonEnabled)
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as
                    TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,textDecorations);
            }
        }
    }
}