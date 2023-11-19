using System;
using System.Windows;
using System.Windows.Threading;
using System.Data.SqlClient;

namespace MyWindowProject
{
    public partial class MainMenu : Window
    {

        private DispatcherTimer timer;
        public MainMenu()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start(); 
            
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
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.ShowDialog();
            this.Close();
        }
        
    }
}
