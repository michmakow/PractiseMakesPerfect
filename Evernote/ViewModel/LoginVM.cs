using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Evernote.Annotations;
using Evernote.Model;
using Evernote.ViewModel.Commands;

namespace Evernote.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private bool _isShowingRegister = false;
        private User _user;

        public User User
        {
            get => _user;
            set => _user = value;
        }

        private Visibility _loginVisibility;

        public Visibility LoginVisibility
        {
            get => _loginVisibility;
            set
            {
                _loginVisibility = value; 
                OnPropertyChanged(nameof(LoginVisibility));
            }
        }

        private Visibility _registerVisibility;

        public Visibility RegisterVisibility
        {
            get => _registerVisibility;
            set
            {
                _registerVisibility = value;
                OnPropertyChanged(nameof(RegisterVisibility));
            }
        }



        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand{ get; set; }
        public ShowRegisterCommand ShowRegisterCommand{ get; set; }

        public LoginVM()
        {
            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);
        }

        public void SwitchViews()
        {
            _isShowingRegister = !_isShowingRegister;

            if (_isShowingRegister)
            {
                RegisterVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
            else
            {
                RegisterVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
