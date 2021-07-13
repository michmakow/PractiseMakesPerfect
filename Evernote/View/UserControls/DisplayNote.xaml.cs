﻿using System.Windows;
using System.Windows.Controls;
using Evernote.Model;

namespace Evernote.View.UserControls
{
    /// <summary>
    /// Interaction logic for DisplayNote.xaml
    /// </summary>
    public partial class DisplayNote : UserControl
    {
        public Note Note
        {
            get => (Note)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(DisplayNote),
                new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DisplayNote noteUserControl)
            {
                noteUserControl.DataContext = noteUserControl.Note;
            }
        }

        public DisplayNote()
        {
            InitializeComponent();
        }
    }
}
