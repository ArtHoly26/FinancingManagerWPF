using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;


namespace MyWindowProject
{
    public partial class  InfoWindow : Window
    {
        private UserViewModel userViewModel;
        public InfoWindow(UserViewModel userViewModel)
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

        private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            ListBox listBox = (ListBox)sender;
            ListBoxItem selectedListBoxItem = (ListBoxItem)listBox.SelectedItem;

            if (selectedListBoxItem == January)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 1);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за январь месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == February)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 2);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за февраль месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за февраль месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == March)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 3);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за март месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за март месяц {sum} ";
                            }       
                        }
                    }
                }
            }

            if (selectedListBoxItem == April)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 4);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за апрель месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за апрель месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == May)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 5);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за май месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за май месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == June)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 6);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за июнь месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за июнь месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == July)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 7);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за июль месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за июль месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == August)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 8);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за август месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за август месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == September)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 9);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за сентябрь месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за сентябрь месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == October)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 10);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за октябрь месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за октябрь месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == November)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 11);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за ноябрь месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за ноябрь месяц {sum} ";
                            }
                        }
                    }
                }
            }

            if (selectedListBoxItem == December)
            {
                decimal sum = 0;
                using (SqlConnection connection = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = UserDb; Integrated Security = True"))
                {
                    connection.Open();
                    string sql = "SELECT UserId, SUM(Доход) AS TotalValue FROM Income WHERE UserId = @Id AND MONTH(Месяц) = @Month GROUP BY UserId;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Month", 12);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                sum = reader.GetDecimal(1);
                                textIncome.Text = $"Ваш доход за декабрь месяц {sum} ";
                            }
                            if (sum == 0)
                            {
                                textIncome.Text = $"Ваш доход за декабрь месяц {sum} ";
                            }
                        }
                    }
                }
            }




        }
    }
}
