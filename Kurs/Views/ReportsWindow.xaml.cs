using RepairShopIS.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairShopIS.Views
{
    public partial class ReportsWindow : Window
    {
        private readonly RepairShopSystem _system;

        public ReportsWindow(RepairShopSystem system)
        {
            InitializeComponent();
            _system = system;
        }

        private void ClientsReportGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsReportGrid.ItemsSource = _system.GetRegularClients();
        }

        private void GenerateOrdersReport_Click(object sender, RoutedEventArgs e)
        {
            var start = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            var end = EndDatePicker.SelectedDate ?? DateTime.MaxValue;
            OrdersReportGrid.ItemsSource = _system.GetOrdersInPeriod(start, end);
        }

        private void GenerateEmployeesReport_Click(object sender, RoutedEventArgs e)
        {
            var start = StartDateEmpPicker.SelectedDate ?? DateTime.MinValue;
            var end = EndDateEmpPicker.SelectedDate ?? DateTime.MaxValue;
            var stats = _system.GetEmployeeStatistics(start, end);
            EmployeesReportGrid.ItemsSource = stats.Select(s => new
            {
                FullName = s.Item1.FullName,
                Repaired = s.Item2,
                Faulty = s.Item3
            });
        }
    }
}