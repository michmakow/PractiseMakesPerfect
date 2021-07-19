using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginVM VM { get; set; }

        public LoginCommand(LoginVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is User user))
                return false;

            if (string.IsNullOrEmpty(user.Username))
                return false;

            if (string.IsNullOrEmpty(user.Password))
                return false;


            return true;
        }

        public void Execute(object parameter)
        {
            VM.Login();
        }

        public event EventHandler CanExecuteChanged;
    }
}
