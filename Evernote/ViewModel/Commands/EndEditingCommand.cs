using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        public NotesVM ViewModel { get; set; }

        public EndEditingCommand(NotesVM vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Notebook notebook)
            {
                ViewModel.StopEditing(notebook);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}