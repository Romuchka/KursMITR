using RepairShopIS.Interfaces;
using RepairShopIS.Models;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace RepairShopIS.Views
{
    public partial class TelevisionsWindow : Window
    {
        private readonly IRepairShopSystem _system;

        public TelevisionsWindow(IRepairShopSystem system)
        {
            InitializeComponent();
            _system = system;
            RefreshTelevisionsGrid();
        }

        private void RefreshTelevisionsGrid()
        {
            TelevisionsGrid.ItemsSource = null;
            TelevisionsGrid.ItemsSource = _system.Televisions;
        }

        private void AddTelevision_Click(object sender, RoutedEventArgs e)
        {
            var brand = BrandText.Text.Trim();
            var country = CountryText.Text.Trim();
            var manufacturer = ManufacturerText.Text.Trim();
            var photoLink = PhotoLinkText.Text.Trim();

            int usageYears;
            if (!int.TryParse(UsageYearsText.Text.Trim(), out usageYears))
            {
                MessageBox.Show("Срок эксплуатации должен быть числом");
                return;
            }

            if (string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(manufacturer))
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }

            if (!Regex.IsMatch(country, @"^[a-zA-Zа-яА-Я\s]+$"))
            {
                MessageBox.Show("Страна должна содержать только буквы и пробелы");
                return;
            }

            var tv = new Television(brand, country, manufacturer, photoLink, usageYears);
            _system.AddTelevision(tv);
            RefreshTelevisionsGrid();
            ClearInputs();
        }

        private void DeleteTelevision_Click(object sender, RoutedEventArgs e)
        {
            if (TelevisionsGrid.SelectedItem is ITelevision selectedTv)
            {
                _system.RemoveTelevision(selectedTv);
                RefreshTelevisionsGrid();
            }
            else
            {
                MessageBox.Show("Выберите телевизор для удаления");
            }
        }

        private void ClearInputs()
        {
            BrandText.Text = "";
            CountryText.Text = "";
            ManufacturerText.Text = "";
            PhotoLinkText.Text = "";
            UsageYearsText.Text = "";
        }
    }
}