using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Try2.Context;
using Try2.Interfaces;
using Try2.Services.Interfaces;
using Try2.Views.Windows;

namespace Try2.ViewModels
{
    public class AuthViewModel : INotifyPropertyChanged
    {

        public string Title { get; set; } = "Autorization";

        private string _username;
        private string _password;
        private bool _isLoggedIn;
        private string _RegisterEror;
        private bool _LoginEror = false;
        private readonly IRepository<User> _UsersRepository;
        private readonly IRepository<Right> _RightsRepository;

        private string _ColorLabel;

        public string ColorLabel
        {
            get
            {
                return _ColorLabel;
            }
            set
            {
                if (_ColorLabel != value)
                {
                    _ColorLabel = value;
                    OnPropertyChanged(nameof(RegisterEror));
                }
            }
        }

        public string RegisterEror
        {
            get
            {
                return _RegisterEror;
            }
            set
            {
                if (_RegisterEror != value)
                {
                    _RegisterEror = value;
                    OnPropertyChanged(nameof(RegisterEror));
                }
            }
        }

        public bool IsVisible
        {
            get { return _LoginEror; }
            set
            {
                if (_LoginEror != value)
                {
                    _LoginEror = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }


        public event EventHandler CloseWindowRequested;


        bool isPresented = true;
        public bool IsPresented
        {
            get { return isPresented; }
            set { if (isPresented != value) { isPresented = value; OnPropertyChanged(); } }
        }


        public string Number { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public string NewUsername
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string NewPassword
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }



        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(Login, CanLogin);
                }
                return _loginCommand;
            }
        }

        private ICommand _AddNewUser;

        public ICommand AddNewUserCommand => _AddNewUser
            ??= new RelayCommand(OnAddNewUserExecuted, CanAddNewUserCommandExecute);

        private bool CanAddNewUserCommandExecute(object arg)
        {
            return !string.IsNullOrEmpty(NewUsername) && !string.IsNullOrEmpty(NewPassword);
        }

        private void OnAddNewUserExecuted(object obj)
        {
            if (NewUsername.Length < 4 || Number == null || NewPassword.Length < 4)
            {
                RegisterEror = "Не удалось зарегистрировать нового пользователя. Возможно не указан номер или длина пароля/логина меньше 4 символов";
            }
            else
            {
                var new_user = new User
                {
                    Name = NewUsername,
                    Password = NewPassword,
                    Right = _RightsRepository.Items.Where(x => x.Id == 3).FirstOrDefault(),
                };

               var Add = _UsersRepository.Items.Any(x => x.Name == NewUsername);

                if (Add)
                {
                    RegisterEror = "Пользователь с таким именем уже существует";
                }
                else
                {
                    _UsersRepository.Add(new_user);
                    RegisterEror = "Пользователь удачно зарегистрирован";
                }
                
            }
        }

        

        public AuthViewModel(IRepository<Right> Rights, IRepository<User> Users)
        {
            _RightsRepository = Rights;
            _UsersRepository = Users;
            _loginCommand = new RelayCommand(Login, CanLogin);
        }



        private bool CanLogin(object parameter)
        {
            
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void Login(object parameter)
        {
            

            
            var user = _UsersRepository.Items.Where(x => x.Name == Username && x.Password == Password).FirstOrDefault();

            if (user != null)
            {
                IsLoggedIn = true;
                var mainwindow = new MainWindow1(user, _UsersRepository);
                mainwindow.Show();
                IsPresented = false;
                CloseWindowRequested?.Invoke(this, new CloseWindowEventArgs());
            }

            else
            {
                ShowLabel();
            }
        }



        public class CloseWindowEventArgs : EventArgs
        {
           
        }


        private void ShowLabel()
        {
            IsVisible = true;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(2)
            };
            /*
             * 
            // Завершение анимации
            animation.Completed += (sender, e) =>
            {
                Thread.Sleep(10000);
                IsVisible = false;
            };

            */
            // Найти окно, чтобы получить доступ к его ресурсам
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            // Запустить анимацию
            Label label = window?.FindName("LoginEror") as Label;
            label?.BeginAnimation(UIElement.OpacityProperty, animation);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
