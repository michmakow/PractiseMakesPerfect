using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginVM VM { get; set; }

        public RegisterCommand(LoginVM vm)
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

            if (string.IsNullOrEmpty(user.ConfirmPassword))
                return false;

            if (!user.Password.Equals(user.ConfirmPassword))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            VM.Register();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}