using System;
using System.Collections.ObjectModel;
using Evernote.Model;
using Evernote.ViewModel.Commands;
using Evernote.ViewModel.Helpers;

namespace Evernote.ViewModel
{
    public class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selecteNotebook;

        public Notebook SelectedNotebook
        {
            get => _selecteNotebook;
            set
            {
                _selecteNotebook = value;
                //TODO: get notes
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

        public void CreateNotebook()
        {
            DatabaseHelper.Insert(new Notebook
            {
                Name = "New notebook"
            });
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
        }
    }
}
