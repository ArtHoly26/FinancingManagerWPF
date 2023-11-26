using System.Windows;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace MyWindowProject
{
    public class UserViewModel: INotifyPropertyChanged
    {
        private UserData _user;
        public UserData User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public UserViewModel()
        {
            User = new UserData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
