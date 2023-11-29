using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;

namespace MyWindowProject
{
    public class UserData : INotifyPropertyChanged
    {
        private int id;
        private string firstname;
        private string lastname;
        private string email;
        private int age;
        private string login;
        private string password;
        private double money;
        private DateTime date;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [Required(ErrorMessage ="Введите имя!")]
        public string? firstName
        {
            get { return firstname; }
            set
            {
                if (firstname != value)
                {
                    firstname = value;
                    OnPropertyChanged(nameof(firstName));
                }
            }
        }

        [Required(ErrorMessage = "Введите фамилию!")]
        public string? lastName
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged(nameof(lastName));
                }
            }
        }

        [Required(ErrorMessage ="Введите почту!")]
        [EmailAddress]
        public string? Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        [Range(18, 100, ErrorMessage = "Введите возраст 18-100!")]
        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        [Required(ErrorMessage = "Введите логин!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Логин 3-20 символов!")]
        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        [Required(ErrorMessage = "Введите пароль!")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Пароль 4-16 символов!")]
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public double Money
        {
            get { return money; }
            set
            {
                if (money != value)
                {
                    money = value;
                    OnPropertyChanged(nameof(Money));
                }
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Money));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
    
}
