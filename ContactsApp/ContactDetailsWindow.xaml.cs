using System;
using System.Windows;
using ContactsApp.Classes;
using SQLite;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow
    {
        private Contact _contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            _contact = contact;

            NameTextBox.Text = contact.Name;
            EmailTextBox.Text = contact.Email;
            PhoneNumberTextBox.Text = contact.Phone;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            _contact.Name = NameTextBox.Text;
            _contact.Email = EmailTextBox.Text;
            _contact.Phone = PhoneNumberTextBox.Text;

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(_contact);
            }

            Close();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(_contact);
            }

            Close();
        }
    }
}
