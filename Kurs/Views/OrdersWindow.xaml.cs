using RepairShopIS.Models;
using RepairShopIS.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RepairShopIS.Views
{
    public partial class OrdersWindow : Window
    {
        private readonly RepairShopSystem _system;

        public OrdersWindow(RepairShopSystem system)
        {
            InitializeComponent();
            _system = system;
            ClientCombo.ItemsSource = _system.Clients;
            EmployeeCombo.ItemsSource = _system.Employees;
            TelevisionCombo.ItemsSource = _system.Televisions;
            RefreshOrdersGrid();
            RefreshCombos();
        }

        private void RefreshOrdersGrid()
        {
            OrdersGrid.ItemsSource = null;
            OrdersGrid.ItemsSource = _system.Orders;
        }

        private void RefreshCombos()
        {
            CompleteOrderCombo.ItemsSource = null;
            CompleteOrderCombo.ItemsSource = _system.Orders;
            DeleteOrderCombo.ItemsSource = null;
            DeleteOrderCombo.ItemsSource = _system.Orders;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!(ClientCombo.SelectedItem is Client client) ||
                !(EmployeeCombo.SelectedItem is Employee employee) ||
                !(TelevisionCombo.SelectedItem is Television tv))
            {
                MessageBox.Show("Выберите клиента, сотрудника и телевизор");
                return;
            }

            var receiptDate = ReceiptDatePicker.SelectedDate ?? DateTime.Now;
            var fixedIssues = new List<string>(FixedIssuesText.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            decimal cost;
            int warranty;
            if (!decimal.TryParse(CostText.Text, out cost) || !int.TryParse(WarrantyText.Text, out warranty))
            {
                MessageBox.Show("Стоимость и гарантия должны быть числами");
                return;
            }

            var order = new Order(client, employee, tv, receiptDate, fixedIssues, cost, warranty);
            _system.AddOrder(order);
            RefreshOrdersGrid();
            RefreshCombos();
            ClearInputs();
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (CompleteOrderCombo.SelectedItem is Order selectedOrder)
            {
                var issueDate = IssueDatePicker.SelectedDate ?? DateTime.Now;

                // Добавленная проверка на хронологию дат
                if (issueDate < selectedOrder.ReceiptDate)
                {
                    MessageBox.Show("Ошибка: Дата выдачи не может быть раньше даты приема заказа!");
                    return;
                }

                var isFaulty = IsFaultyCheck.IsChecked ?? false;
                selectedOrder.Complete(issueDate, isFaulty);
                RefreshOrdersGrid();
                RefreshCombos();
            }
            else
            {
                MessageBox.Show("Выберите заказ для завершения");
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteOrderCombo.SelectedItem is Order selectedOrder)
            {
                _system.RemoveOrder(selectedOrder);
                RefreshOrdersGrid();
                RefreshCombos();
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления");
            }
        }

        private void ClearInputs()
        {
            ClientCombo.SelectedIndex = -1;
            EmployeeCombo.SelectedIndex = -1;
            TelevisionCombo.SelectedIndex = -1;
            ReceiptDatePicker.SelectedDate = null;
            FixedIssuesText.Text = "";
            CostText.Text = "";
            WarrantyText.Text = "";
        }
    }
}