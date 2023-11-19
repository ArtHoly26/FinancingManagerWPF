using System.Windows;
using System;
using System.ComponentModel;

namespace MyWindowProject
{
    public class UserData: INotifyPropertyChanged
    { 
        public static int Id { get; set; }
        public static string? firstName { get; set; }
        public static string? lastName { get; set; }
        public static string? Email { get; set; }
        public static int Age { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
