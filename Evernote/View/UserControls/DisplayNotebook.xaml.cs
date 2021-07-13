using System.Windows;
using System.Windows.Controls;
using Evernote.Model;

namespace Evernote.View.UserControls
{
    /// <summary>
    /// Interaction logic for DisplayNotebook.xaml
    /// </summary>
    public partial class DisplayNotebook : UserControl
    {
        public Notebook Notebook
        {
            get => (Notebook) GetValue(NotebookProperty);
            set => SetValue(NotebookProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(DisplayNotebook),
                new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DisplayNotebook notebookUserControl)
            {
                notebookUserControl.DataContext = notebookUserControl.Notebook;
            }
        }


        public DisplayNotebook()
        {
            InitializeComponent();
        }
    }
}