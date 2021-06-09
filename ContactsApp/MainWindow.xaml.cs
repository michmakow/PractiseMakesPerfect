using System.Windows;

namespace ContactsApp
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
            var newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();
        }
    }
}
