using System;
using System.Text;
using System.Windows;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace MyWindowProject
{

    public partial class PasswordChangeWindow : Window
    {
        private UserViewModel userViewModel;
        public PasswordChangeWindow(UserViewModel userViewModel)
        {
            InitializeComponent();
            this.userViewModel = userViewModel;
            DataContext = this.userViewModel;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow(userViewModel);
            accountWindow.Show();
            this.Close();

        }

        private void ChangePasswordCheck(object sender, RoutedEventArgs e)
        {
            string passwordEnterOld = oldPassword.Password;
            string passwordEnterNew = newPassword.Password;

            string query = "UPDATE Users SET Пароль=@NewPassword WHERE Логин=@Login AND Пароль=@OldPassword";

            using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", userViewModel.User.Login);
                    command.Parameters.AddWithValue("@OldPassword", passwordEnterOld);
                    command.Parameters.AddWithValue("@NewPassword", passwordEnterNew);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пароль успешно изменен!");
                        AccountWindow accountWindow = new AccountWindow(userViewModel);
                        accountWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        textError.Text = "Ошибка ввода старых данных!";
                    }
                }

            }
        }
    }

}