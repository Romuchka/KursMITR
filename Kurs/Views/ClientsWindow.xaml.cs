using RepairShopIS.Interfaces;
using RepairShopIS.Models;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace RepairShopIS.Views
{
    public partial class ClientsWindow : Window
    {
        private readonly IRepairShopSystem _system;

        public ClientsWindow(IRepairShopSystem system)
        {
            InitializeComponent();
            _system = system;
            RefreshClientsGrid();
        }

        private void RefreshClientsGrid()
        {
            ClientsGrid.ItemsSource = null;
            ClientsGrid.ItemsSource = _system.Clients;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameText.Text.Trim();
            var address = AddressText.Text.Trim();
            var phone = PhoneText.Text.Trim();
            var discountCard = DiscountCardText.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Заполните обязательные поля: ФИО, Адрес, Телефон");
                return;
            }

            if (!Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-Я\s]+$"))
            {
                MessageBox.Show("ФИО должно содержать только буквы и пробелы");
                return;
            }

            if (!Regex.IsMatch(phone, @"^[\d+()\-\s]+$"))
            {
                MessageBox.Show("Телефон должен содержать только цифры, +, -, (, ), пробелы");
                return;
            }

            var client = new Client(fullName, address, phone, discountCard);
            _system.AddClient(client);
            RefreshClientsGrid();
            ClearInputs();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem is IClient selectedClient)
            {
                _system.RemoveClient(selectedClient);
                RefreshClientsGrid();
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления");
            }
        }

        private void ClearInputs()
        {
            FullNameText.Text = "";
            AddressText.Text = "";
            PhoneText.Text = "";
            DiscountCardText.Text = "";
        }
    }
}