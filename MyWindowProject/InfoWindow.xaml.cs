using System.Windows;


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
    }
}
