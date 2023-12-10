using LiveCharts;
using System;
using System.Windows;
using System.Windows.Threading;

namespace MyWindowProject
{
    public partial class MainMenu : Window
    {
        private UserViewModel userViewModel;

        private DispatcherTimer timer;
        public MainMenu(UserViewModel userViewModel)
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            this.userViewModel = userViewModel;
            DataContext = this.userViewModel;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void PerconalAccount(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow(userViewModel);
            accountWindow.ShowDialog();
            this.Close();
        }
        private void IncomeMonth(object sender, RoutedEventArgs e)
        {
            IncomeWindow incomeMonth = new IncomeWindow(userViewModel);
            incomeMonth.ShowDialog();
            this.Close();
        }

        private void FinancingInfo(object sender, RoutedEventArgs e)
        {
            
            InfoWindow infoWindow = new InfoWindow(userViewModel);
            infoWindow.ShowDialog();
            this.Close();
        }

        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            ExpensesWindow expensesWindow = new ExpensesWindow(userViewModel);
            expensesWindow.ShowDialog();
            this.Close();
        }
    }
}

