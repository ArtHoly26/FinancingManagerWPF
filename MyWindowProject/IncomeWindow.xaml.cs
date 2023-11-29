using System;
using System.Windows;
using System.Data.SqlClient;


namespace MyWindowProject
{
    public partial class IncomeWindow : Window
    {
        private UserViewModel userViewModel;
        public IncomeWindow(UserViewModel userViewModel)
        {
            InitializeComponent();
            this.userViewModel = userViewModel;
            DataContext = this.userViewModel;
        }

        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(userViewModel);
            mainMenu.Show();
            this.Close();
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            string localMoney = textMoney.Text;
            DateTime localDate = datePicker.DisplayDate;
   
              try
              {
                    using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                    {

                        connection.Open();
                        string query = "INSERT INTO Income (UserId,Доход,Месяц)" +
                                           "VALUES (@Value1, @Value2, @Value3)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Value1", userViewModel.User.Id);
                            command.Parameters.AddWithValue("@Value2", localMoney);
                            command.Parameters.AddWithValue("@Value3", localDate);
                            
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
}
