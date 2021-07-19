using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Evernote.ViewModel;
using Evernote.ViewModel.Helpers;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Evernote.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private NotesVM _viewModel;
        public NotesWindow()
        {
            InitializeComponent();

            _viewModel = Resources["Vm"] as NotesVM;
            _viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;
            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontFamilyComboBox.ItemsSource = fontFamilies;

            var fontSizes = new List<double> {8, 9, 10, 11, 12, 14, 16, 28, 48};
            FontSizeComboBox.ItemsSource = fontSizes;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!string.IsNullOrEmpty(App.UserId)) return;

            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            _viewModel.GetNotebooks();
        }

        private void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            ContentRichTextBox.Document.Blocks.Clear();
            if (_viewModel.SelectedNote != null)
            {
                if(!string.IsNullOrEmpty(_viewModel.SelectedNote.FileLocation))
                {
                    var fileStream = new FileStream(_viewModel.SelectedNote.FileLocation, FileMode.Open);
                    var content = new TextRange(ContentRichTextBox.Document.ContentStart,
                        ContentRichTextBox.Document.ContentEnd);
                    content.Load(fileStream, DataFormats.Rtf);
                }
            }
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

            var selectedStyle = ContentRichTextBox.Selection.GetPropertyValue(FontStyleProperty);
            ItalicButton.IsChecked = selectedStyle != DependencyProperty.UnsetValue && selectedStyle.Equals(FontStyles.Italic);

            var selectedDecoration = ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineButton.IsChecked = selectedDecoration != DependencyProperty.UnsetValue && selectedDecoration.Equals(TextDecorations.Underline);

            FontFamilyComboBox.SelectedItem = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontSizeComboBox.Text = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
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

        private void FontFamilyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeComboBox.Text);
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{_viewModel.SelectedNote.Id}.rtf");
            _viewModel.SelectedNote.FileLocation = rtfFile;
            DatabaseHelper.Update(_viewModel.SelectedNote);

            var fileStream = new FileStream(rtfFile, FileMode.Create);
            var content = new TextRange(ContentRichTextBox.Document.ContentStart,ContentRichTextBox.Document.ContentEnd);
            content.Save(fileStream, DataFormats.Rtf);
        }
    }
}