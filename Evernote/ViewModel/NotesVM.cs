using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Evernote.Annotations;
using Evernote.Model;
using Evernote.ViewModel.Commands;
using Evernote.ViewModel.Helpers;

namespace Evernote.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selecteNotebook;

        public Notebook SelectedNotebook
        {
            get => _selecteNotebook;
            set
            {
                _selecteNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            GetNotebooks();
        }

        public void CreateNotebook()
        {
            DatabaseHelper.Insert(new Notebook
            {
                Name = "New notebook"
            });

            GetNotebooks();
        }
        public void CreateNote(int notebookId)
        {
            DatabaseHelper.Insert(new Note
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Title = "New note"
            });

            GetNotes();
        }

        private void GetNotebooks()
        {
            var notebooks = DatabaseHelper.Read<Notebook>();

            Notebooks.Clear();
            foreach (var notebook in notebooks) Notebooks.Add(notebook);
        }

        private void GetNotes()
        {
            if (SelectedNotebook == null) return;
            
            var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

            Notes.Clear();
            foreach (var note in notes) Notes.Add(note);
        }

       
    }
}
