using RepairShopIS.Interfaces;
using RepairShopIS.Models;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace RepairShopIS.Views
{
    public partial class EmployeesWindow : Window
    {
        private readonly IRepairShopSystem _system;

        public EmployeesWindow(IRepairShopSystem system)
        {
            InitializeComponent();
            _system = system;
            RefreshEmployeesGrid();
        }

        private void RefreshEmployeesGrid()
        {
            EmployeesGrid.ItemsSource = null;
            EmployeesGrid.ItemsSource = _system.Employees;
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameText.Text.Trim();
            var address = AddressText.Text.Trim();
            var phone = PhoneText.Text.Trim();
            var specialty = SpecialtyText.Text.Trim();

            int age;
            int experience;
            if (!int.TryParse(AgeText.Text.Trim(), out age) || !int.TryParse(ExperienceText.Text.Trim(), out experience))
            {
                MessageBox.Show("Возраст и стаж должны быть числами");
                return;
            }

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(specialty))
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }

            if (!Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-Я\s]+$"))
            {
                MessageBox.Show("ФИО должно содержать только буквы и пробелы");
                return;
            }

            if (!Regex.IsMatch(specialty, @"^[a-zA-Zа-яА-Я\s]+$"))
            {
                MessageBox.Show("Специальность должна содержать только буквы и пробелы");
                return;
            }

            if (!Regex.IsMatch(phone, @"^[\d+()\-\s]+$"))
            {
                MessageBox.Show("Телефон должен содержать только цифры, +, -, (, ), пробелы");
                return;
            }

            var employee = new Employee(fullName, age, address, phone, specialty, experience);
            _system.AddEmployee(employee);
            RefreshEmployeesGrid();
            ClearInputs();
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is IEmployee selectedEmployee)
            {
                _system.RemoveEmployee(selectedEmployee);
                RefreshEmployeesGrid();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления");
            }
        }

        private void ClearInputs()
        {
            FullNameText.Text = "";
            AgeText.Text = "";
            AddressText.Text = "";
            PhoneText.Text = "";
            SpecialtyText.Text = "";
            ExperienceText.Text = "";
        }
    }
}