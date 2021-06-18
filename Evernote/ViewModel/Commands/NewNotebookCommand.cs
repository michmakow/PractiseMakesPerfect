using System;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NotesVM VM{ get; set; }

        public NewNotebookCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            VM.CreateNotebook();
        }

        public event EventHandler CanExecuteChanged;
    }
}
