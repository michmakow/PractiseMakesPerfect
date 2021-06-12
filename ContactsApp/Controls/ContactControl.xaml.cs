using System.Windows;
using System.Windows.Controls;
using ContactsApp.Classes;

namespace ContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public Contact Contact
        {
            get { return (Contact) GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl),
                new PropertyMetadata(new Contact() {Email = "Email", Name = "Name Lastname", Phone = "(123) 456 789"},
                    SetTest));

        private static void SetTest(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ContactControl;

            if (control != null)
            {
                control.NameTextBlock.Text = (e.NewValue as Contact)?.Name;
                control.EmailTextBlock.Text = (e.NewValue as Contact)?.Email;
                control.PhoneTextBlock.Text = (e.NewValue as Contact)?.Phone;
            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}