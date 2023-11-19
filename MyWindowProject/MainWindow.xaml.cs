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
            string loginToCheck = textLogin.Text;
            string passwordToCheck = textPassword.Text;
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
                                    GetInfo(loginToCheck);
                                    MainMenu taskWindow = new MainMenu();
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
        private void GetInfo(string choiseLogin)
        {
            using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT Id, Имя, Фамилия, Почта, Возраст, Логин, Пароль FROM Users WHERE Логин=@Login";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", choiseLogin);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserData.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            UserData.firstName = reader["Имя"].ToString();
                            UserData.lastName = reader["Фамилия"].ToString();
                            UserData.Email = reader["Почта"].ToString();
                            UserData.Login = reader["Логин"].ToString();
                            UserData.Password = reader["Пароль"].ToString();
                            if (int.TryParse(reader["Возраст"].ToString(), out int age))
                            {
                                UserData.Age = age;
                            }
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
        private void TextBox_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textLogin.Text))
            {
                checkBox1.IsChecked = true;

            }
            else
            {
                checkBox1.IsEnabled = false;
                checkBox1.IsChecked = false;
            }
        }
        private void TextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textPassword.Text))
            {
                checkBox2.IsChecked = true;
            }
            else
            {
                checkBox2.IsEnabled = false;
                checkBox2.IsChecked = false;
            }
        }
   
    }
}