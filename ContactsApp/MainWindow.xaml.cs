using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ContactsApp.Classes;
using SQLite;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<Contact> _contacts;

        public MainWindow()
        {
            InitializeComponent();

            _contacts = new List<Contact>();

            ReadDatabase();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using var connection = new SQLiteConnection(App.DatabasePath);
            connection.CreateTable<Contact>();
            _contacts = connection.Table<Contact>().OrderBy(c =>c.Name).ToList();

            if (_contacts != null)
                ContactsListView.ItemsSource = _contacts;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextBox = (TextBox) sender;

            var filteredList = _contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            ContactsListView.ItemsSource = filteredList;
        }

        private void ContactsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedContact = (Contact) ContactsListView.SelectedItem;

            if (selectedContact != null)
            {
                var cdw = new ContactDetailsWindow(selectedContact);
                cdw.ShowDialog();

                ReadDatabase();
            }
        }
    }
}