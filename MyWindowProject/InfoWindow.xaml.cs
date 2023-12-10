using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Text;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using LiveCharts.Wpf.Charts.Base;

namespace MyWindowProject
{
    public partial class InfoWindow : Window
    {
        private UserViewModel userViewModel;
        private Dictionary<ListBoxItem, int> monthMapping;
      
        public InfoWindow(UserViewModel userViewModel)
        {
            InitializeComponent();
            InitializeMonthMapping();

            this.userViewModel = userViewModel;
            DigIncome();
            DataContext = this.userViewModel;

        }

        private void DigIncome()
        {
            using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
            {
                connection.Open();
                userViewModel.IncomeSeries=new SeriesCollection();
                

                string sql = "SELECT MONTH(Месяц) AS НомерМесяца, SUM(Доход) AS ДоходЗаМесяц " +
                             "FROM Income " +
                             "WHERE UserId = @UserId " +
                             "GROUP BY MONTH(Месяц);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userViewModel.User.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        userViewModel.XAxes = new List<Axis>
                        {
                            new Axis { Title = "Месяц"}
                        };

                        userViewModel.YAxes = new List<Axis>
                        {
                            new Axis { Title = "Доход" }
                        };
                        while (reader.Read())
                        {
                            int month = reader.GetInt32(reader.GetOrdinal("НомерМесяца"));
                            decimal incomeForMonth = reader.GetDecimal(reader.GetOrdinal("ДоходЗаМесяц"));
                            string monthString= MonthConvert(month);

                            var columnSeries = new ColumnSeries
                            {
                                Values = new ChartValues<decimal> { incomeForMonth },
                                Title = monthString
                            };

                            userViewModel.IncomeSeries.Add(columnSeries);
                        }

                        

                    }
                }
            }
        }
                
        private string MonthConvert(int _month)
        {
            string localMonth= string.Empty;

            if (_month == 1) { localMonth = "Январь"; }
            if (_month == 2) { localMonth = "Февраль"; }
            if (_month == 3) { localMonth = "Март"; }
            if (_month == 4) { localMonth = "Апрель"; }
            if (_month == 5) { localMonth = "Май"; }
            if (_month == 6) { localMonth = "Июнь"; }
            if (_month == 7) { localMonth = "Июль"; }
            if (_month == 8) { localMonth = "Август"; }
            if (_month == 9) { localMonth = "Сентябрь"; }
            if (_month == 10) { localMonth = "Октябрь"; }
            if (_month == 11) { localMonth = "Ноябрь"; }
            if (_month == 12) { localMonth = "Декабрь"; }

            return localMonth;
        }
        
        private void InitializeMonthMapping()
        {
            monthMapping = new Dictionary<ListBoxItem, int>
               {
                    { January, 1 },
                    { February, 2 },
                    { March, 3 },
                    { April, 4 },
                    { May, 5 },
                    { June, 6 },
                    { July, 7 },
                    { August, 8 },
                    { September, 9 },
                    { October, 10 },
                    { November, 11 },
                    { December, 12 },
               };
        }
        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(userViewModel);
            mainMenu.Show();
            this.Close();
        }
        private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            ListBoxItem selectedListBoxItem = (ListBoxItem)listBox.SelectedItem;

            if (monthMapping.TryGetValue(selectedListBoxItem, out int selectedMonth))
            {
                decimal sum = 0;
  
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(selectedMonth);

                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();

                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", selectedMonth);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за {monthName} месяц {sum} ";
                            }

                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за {monthName} месяц {sum} ";
                            }
                        }
                    }
                }

                using (SqlConnection connection2 = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection2.Open();
                    userViewModel.PieSeriesCollection = new SeriesCollection();
                    string category = "Нет";

                    string sql2 = "SELECT Категория, SUM(Расходы) AS Сумма " +
                         "FROM Expenses " +
                         "WHERE UserId = @Id AND MONTH(Дата)=@Month "+
                         "GROUP BY Категория " +
                         "ORDER BY Категория;";

                    StringBuilder resultBuilder = new StringBuilder();

                    using (SqlCommand command2 = new SqlCommand(sql2, connection2))
                    {
                        command2.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command2.Parameters.AddWithValue("@Month", selectedMonth);

                        using (SqlDataReader reader = command2.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                category = reader.GetString(0);
                                sum = reader.GetDecimal(1);
                                resultBuilder.AppendLine($"Ваши расходы за месяц по категории {category} - {sum} ");

                                PieSeries pieSeries = new PieSeries
                                {
                                    Title = category,
                                    Values = new ChartValues<decimal> { sum }
                                };

                                userViewModel.PieSeriesCollection.Add(pieSeries);
                            }

                            textExpenses.Text = resultBuilder.ToString();

                            if (sum == 0)
                            {
                                textExpenses.Text = $"Ваши расходы за  месяц по категории {category} - {sum} ";
                            }
                        }
                    }
                }
            }

        }
    }
    
}
