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
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
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

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                User = new User
                {
                    Username = _username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                User = new User
                {
                    Username = this.Username,
                    Password = _password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = _name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _lastname;

        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                User = new User
                {
                    Username = _username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = _lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = _confirmPassword
                };
                OnPropertyChanged(nameof(ConfirmPassword));
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

            User = new User();
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

        public  void Login()
        {
            //TODO 
        }

        public void Register()
        {
            //TODO
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
