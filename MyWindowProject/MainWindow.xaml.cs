using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Security.Cryptography;

namespace MyWindowProject
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Enter(object sender, RoutedEventArgs e)
        {
            UserViewModel userViewModel = new UserViewModel();
            string loginToCheck = textLogin.Text;
            string passwordToCheck = textPassword.Password;
            textError.Text = string.Empty;
            using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT Id, Пароль From Users WHERE Логин=@Login";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", loginToCheck);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Найден пользователь с указанным логином
                            int userId = reader.GetInt32(0);
                            string hashedPasswordFromDatabase = reader.GetString(1);

                            // Сравниваем хэшированный пароль
                            using (SHA256 sha256 = SHA256.Create())
                            {
                                byte[] hashedBytesToTextBox = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordToCheck));
                                byte[] hashedBytesTodDatabase = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashedPasswordFromDatabase));

                                string hashedPasswordToCheck = BitConverter.ToString(hashedBytesToTextBox).Replace("-", "");
                                hashedPasswordFromDatabase = BitConverter.ToString(hashedBytesTodDatabase).Replace("-", "");
                               
                                if (hashedPasswordToCheck == hashedPasswordFromDatabase)
                                {
                                    // Пароли совпадают, пользователь аутентифицирован
                                    using (SqlConnection connection1 = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                                    {
                                        connection1.Open();
                                        string query1 = "SELECT Id, Имя, Фамилия, Почта, Возраст, Логин, Пароль FROM Users WHERE Логин=@Login";

                                        using (SqlCommand command1 = new SqlCommand(query1, connection1))
                                        {
                                            command1.Parameters.AddWithValue("@Login", loginToCheck);

                                            using (SqlDataReader reader1 = command1.ExecuteReader())
                                            {
                                                while (reader1.Read())
                                                {
                                                    userViewModel = new UserViewModel
                                                    {
                                                        User = new UserData
                                                        {
                                                            Id = reader1.GetInt32(reader1.GetOrdinal("Id")),
                                                            firstName = reader1["Имя"].ToString(),
                                                            lastName = reader1["Фамилия"].ToString(),
                                                            Email = reader1["Почта"].ToString(),
                                                            Login = reader1["Логин"].ToString(),
                                                            Password = reader1["Пароль"].ToString(),
                                                            Age = reader1.GetInt32(reader1.GetOrdinal("Возраст"))
                                                        }
                                                    };
                                                };
                                            }
                                        }
                                    }
                                    MainMenu taskWindow = new MainMenu(userViewModel);
                                    taskWindow.Show();
                                    this.Close();
                                }
                                else
                                {
                                    // Пароли не совпадают
                                    string errorMesage = "Неверный логин или пароль!";
                                    textError.Text = errorMesage;
                                }
                            }
                        }
                        else
                        {
                            // Пользователь с указанным логином не найден
                            string errorMessage = "Неверный логин или пароль!";
                            textError.Text = errorMessage;
                        }
                    }
                }
            }
        }
        private void Register(object sender, RoutedEventArgs e)
        {
            RegisterWindow taskWindow = new RegisterWindow();
            taskWindow.Show();
            this.Close();
        }
        
    }
}