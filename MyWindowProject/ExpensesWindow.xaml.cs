using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Windows.Input;

namespace MyWindowProject
{
    public partial class ExpensesWindow : Window
    {
        private UserViewModel userViewModel;
        private Dictionary<ListBoxItem, string> categoryMapping;
        private ListBoxItem selectedListBoxItem;
        public ExpensesWindow(UserViewModel viewViewModel)
        {
            InitializeComponent();
            InitializeCategoryMapping();
            this.userViewModel = viewViewModel;
            DataContext = this.userViewModel;
        }

        private void InitializeCategoryMapping()
        {
               categoryMapping = new Dictionary<ListBoxItem, string>
               {
                    { Product, "Продукты" },
                    { Auto, "Автомобиль/Бензин/Проезды"},
                    { Internet, "Интернет/Связь" },
                    { Clothes, "Одежда"},
                    { Entertainments, "Развлечения/Праздники" },
                    { Beauty, "Красота" },
                    { Education, "Образование"},
                    { Communal_services, "Коммунальные платежи" },
                    { Credit, "Кредиты/Аренда" },
                    { Health, "Здоровье/Лекарства" },
                    { Technic, "Техника/Электроника" },
                    { Other, "Прочее" },
               };
        }
        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(userViewModel);
            mainMenu.Show();
            this.Close();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            selectedListBoxItem = (ListBoxItem)listBox.SelectedItem;
        }
        private void SaveData(object sender, RoutedEventArgs e)
        {
            string localExpenses = textExpenses.Text;
            DateTime? selactedDate = datePicker.SelectedDate;

            if (categoryMapping.TryGetValue(selectedListBoxItem, out string selectedCategory))
            {
                string categoryName = selectedCategory;

                try
                {
                    using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                    {

                        connection.Open();
                        string query = "INSERT INTO Expenses (UserId, Расходы, Категория, Дата)" +
                                           "VALUES (@Value1, @Value2, @Value3 , @Value4)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Value1", userViewModel.User.Id);
                            command.Parameters.AddWithValue("@Value2", localExpenses);
                            command.Parameters.AddWithValue("@Value3", categoryName);
                            command.Parameters.AddWithValue("@Value4", selactedDate);

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
}
    
