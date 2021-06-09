using System.Windows;
using ContactsApp.Classes;
using SQLite;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(new Contact
                {
                    Name = NameTextBox.Text,
                    Email = EmailTextBox.Text,
                    Phone = PhoneNumberTextBox.Text
                });
            }

            Close();
        }
    }
}
