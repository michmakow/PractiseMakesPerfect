using System;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public LoginVM ViewModel{ get; set; }

        public ShowRegisterCommand(LoginVM vm)
        {
            ViewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.SwitchViews();
        }

        public event EventHandler CanExecuteChanged;
    }
}
