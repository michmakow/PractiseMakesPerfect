using System;
using System.IO;
using System.Windows;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly string DatabaseName = "Contacts.db";
        public static string DatabasePath { get; } = Path.Combine(Environment.CurrentDirectory, DatabaseName);
    }
}
