using RepairShopIS.Services;
using RepairShopIS.Views;
using System.Windows;
using System.ComponentModel;

namespace RepairShopIS
{
    public partial class MainWindow : Window
    {
        private readonly RepairShopSystem _system = new RepairShopSystem();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            new ClientsWindow(_system).ShowDialog();
        }

        private void ManageEmployees_Click(object sender, RoutedEventArgs e)
        {
            new EmployeesWindow(_system).ShowDialog();
        }

        private void ManageTelevisions_Click(object sender, RoutedEventArgs e)
        {
            new TelevisionsWindow(_system).ShowDialog();
        }

        private void ManageOrders_Click(object sender, RoutedEventArgs e)
        {
            new OrdersWindow(_system).ShowDialog();
        }

        private void ViewReports_Click(object sender, RoutedEventArgs e)
        {
            new ReportsWindow(_system).ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _system.Save();
        }
    }
}