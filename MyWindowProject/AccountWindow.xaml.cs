using System.Data.SqlClient;
using System.Windows;
using System;
using System.Windows.Controls;

namespace MyWindowProject
{
    public partial class AccountWindow : Window
    {
        public AccountWindow()
        {
            InitializeComponent();
            DataContext = UserData.Login;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            
            string newFirstName=textFirstName.Text;
            string newLastName=textLastName.Text;
            string newEmail=textEmail.Text;
            string newLogin=textLogin.Text;

            if (int.TryParse(textAge.Text, out int intAge))
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                    {
                        connection.Open();

                        string updateSql = "UPDATE Users SET Имя = @FirstName, Фамилия = @LastName, Возраст=@Age, Почта=@Email, Логин=@Login WHERE Id = @Id";
                        using (SqlCommand command = new SqlCommand(updateSql, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", newFirstName);
                            command.Parameters.AddWithValue("@LastName", newLastName);
                            command.Parameters.AddWithValue("@Age", intAge);
                            command.Parameters.AddWithValue("@Email", newEmail);
                            command.Parameters.AddWithValue("@Login", newLogin);
                            command.Parameters.AddWithValue("@Id", UserData.Id);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Данные успешно обновленны в базу данных.");
                                MainMenu mainMenu = new MainMenu();
                                mainMenu.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Не удалось обновить данные в базе данных.");
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
    }
}
