using System.Windows;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyWindowProject
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        { 
            InitializeComponent();
            UserViewModel userViewModel = new UserViewModel();
            DataContext= userViewModel;
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow taskWindow=new MainWindow();
            taskWindow.Show();
            this.Close();
        }
        private void SaveUserData()
        {
           
            string localFirstName = textFirstName.Text;
            string localLastName= textLastName.Text;
            string localEmail= textEmail.Text;
            string localLogin = textLogin.Text;
            string localPassword = boxPassword.Password;
            
            if (int.TryParse(textAge.Text, out int intAge))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                    {
                        
                        connection.Open();
                        string query = "INSERT INTO Users (Имя,Фамилия,Почта,Возраст,Логин,Пароль)" +
                                           "VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Value1", localFirstName);
                            command.Parameters.AddWithValue("@Value2", localLastName);
                            command.Parameters.AddWithValue("@Value3", localEmail);
                            command.Parameters.AddWithValue("@Value4", intAge);
                            command.Parameters.AddWithValue("@Value5", localLogin);
                            command.Parameters.AddWithValue("@Value6", localPassword);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Данные успешно добавлены в базу данных.");
                            }
                            else
                            {
                                MessageBox.Show("Не удалось добавить данные в базу данных.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }
        private void Registration(object sender, RoutedEventArgs e)
        {
            UserViewModel viewModel = DataContext as UserViewModel;

            viewModel.User.Password = boxPassword.Password;
            string userPassword = boxPassword.Password;
            string userRepeatPassword = textPasswordRepeat.Password;

            var validationContext = new ValidationContext(viewModel.User, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(viewModel.User, validationContext, validationResults, validateAllProperties: true);

            if (isValid && userPassword == userRepeatPassword)
            {
                SaveUserData();
                Exit(sender, e);
            }
            else
            {
                textErrorFirstName.Text = validationResults.FirstOrDefault(r => r.MemberNames.Contains(nameof(UserData.firstName)))?.ErrorMessage ?? "";
                textErrorLastName.Text = validationResults.FirstOrDefault(r => r.MemberNames.Contains(nameof(UserData.lastName)))?.ErrorMessage ?? "";
                textErrorAge.Text = validationResults.FirstOrDefault(r => r.MemberNames.Contains(nameof(UserData.Age)))?.ErrorMessage ?? "";
                textErrorEmail.Text = validationResults.FirstOrDefault(r => r.MemberNames.Contains(nameof(UserData.Email)))?.ErrorMessage ?? "";
                textErrorLogin.Text = validationResults.FirstOrDefault(r => r.MemberNames.Contains(nameof(UserData.Login)))?.ErrorMessage ?? "";
                textErrorPassword.Text = validationResults.FirstOrDefault(r => r.MemberNames.Contains(nameof(UserData.Password)))?.ErrorMessage ?? "";
                if (userPassword!= userRepeatPassword)
                {
                    string errorMessage = "Пароль не совпадает";
                    textErrorRepeatPassword.Text = errorMessage;
                }
                

            }      
        }   
        
    }
}
