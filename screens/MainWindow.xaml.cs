using System.Windows;

namespace AIC_shop.screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manage manageWindow = new Manage();
            manageWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sell sellWindow = new Sell();
            sellWindow.ShowDialog();
        }
    }
}