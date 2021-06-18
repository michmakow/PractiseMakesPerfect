using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesVM VM { get; set; }

        public NewNoteCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is Notebook;
        }

        public void Execute(object parameter)
        {
           VM.CreateNote((parameter as Notebook).Id);
        }

        public event EventHandler CanExecuteChanged;
    }
}
