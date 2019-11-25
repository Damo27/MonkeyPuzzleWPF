using MonkeyPuzzleWPF.Models;
using MonkeyPuzzleWPF.Utilities;
using MonkeyPuzzleWPF.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Runtime.CompilerServices;

namespace MonkeyPuzzleWPF.ViewModels
{
    class UserVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _userID;
        private string _password;
        private User _user;
        private Login login;

        [Required]
        public string UserID { get => _userID; set { _userID = value; User.UserID = value; OnPropertyChanged("UserID"); } }

        [Required]
        public string Password { get => _password; set { _password = value; User.Password = value; OnPropertyChanged("Password"); } }

        public User User { get => _user; set => _user = value; }

        public ICommand SignIn { get; private set; }


        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults)
                    )
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public UserVM(Login login)
        {
            User = new User();
            SignIn = new SignInCommand(this);
            this.login = login;
        }
        class SignInCommand : ICommand
        {
            UserVM localUserVM;

            public SignInCommand(UserVM userVM)
            {
                this.localUserVM = userVM;
                localUserVM.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return !string.IsNullOrEmpty(localUserVM.UserID) && !string.IsNullOrEmpty(localUserVM.Password);
            }

            public void Execute(object parameter)
            {
                User tempUser = Connection.GetUser(localUserVM.UserID, localUserVM.Password);
                if (localUserVM.User == tempUser)
                {
                    if (tempUser.AccessGroup <= 1)
                    {
                        var lecturerMain = new LecturerMain(tempUser);
                        localUserVM.login.Hide();
                        lecturerMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("Access denied!", "Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect User ID and Password!", "Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
