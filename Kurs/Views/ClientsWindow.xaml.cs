using RepairShopIS.Models;
using RepairShopIS.Services;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace RepairShopIS.Views
{
    public partial class ClientsWindow : Window
    {
        private readonly RepairShopSystem _system;

        public ClientsWindow(RepairShopSystem system)
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

            // Проверка ФИО: только буквы и пробелы (кириллица и латиница)
            if (!Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-Я\s]+$"))
            {
                MessageBox.Show("ФИО должно содержать только буквы и пробелы");
                return;
            }

            // Проверка телефона: только цифры, +, -, (, ), пробелы
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
            if (ClientsGrid.SelectedItem is Client selectedClient)
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