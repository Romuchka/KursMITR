using RepairShopIS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace RepairShopIS.Services
{
    public class RepairShopSystem
    {
        private readonly ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private readonly ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        private readonly ObservableCollection<Television> _televisions = new ObservableCollection<Television>();
        private readonly ObservableCollection<Order> _orders = new ObservableCollection<Order>();

        private const string DataFilePath = "data.json";

        public RepairShopSystem()
        {
            Load();
        }

        public ReadOnlyObservableCollection<Client> Clients
        {
            get { return new ReadOnlyObservableCollection<Client>(_clients); }
        }

        public ReadOnlyObservableCollection<Employee> Employees
        {
            get { return new ReadOnlyObservableCollection<Employee>(_employees); }
        }

        public ReadOnlyObservableCollection<Television> Televisions
        {
            get { return new ReadOnlyObservableCollection<Television>(_televisions); }
        }

        public ReadOnlyObservableCollection<Order> Orders
        {
            get { return new ReadOnlyObservableCollection<Order>(_orders); }
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void RemoveClient(Client client)
        {
            _clients.Remove(client);
        }

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            _employees.Remove(employee);
        }

        public void AddTelevision(Television tv)
        {
            _televisions.Add(tv);
        }

        public void RemoveTelevision(Television tv)
        {
            _televisions.Remove(tv);
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            _orders.Remove(order);
        }

        public IEnumerable<Order> GetOrdersInPeriod(DateTime from, DateTime to)
        {
            return _orders.Where(o => o.ReceiptDate >= from && o.ReceiptDate <= to);
        }

        public IEnumerable<Client> GetRegularClients()
        {
            return _clients.Where(c => c.IsRegular);
        }

        public IEnumerable<Tuple<Employee, int, int>> GetEmployeeStatistics(DateTime from, DateTime to)
        {
            return _employees.Select(emp =>
            {
                var relevant = _orders
                    .Where(o => o.Executor == emp && o.IsCompleted && o.IssueDate >= from && o.IssueDate <= to)
                    .ToList();

                return new Tuple<Employee, int, int>(emp, relevant.Count, relevant.Count(o => o.IsFaulty));
            });
        }

        private class AppData
        {
            public List<Client> Clients { get; set; }
            public List<Employee> Employees { get; set; }
            public List<Television> Televisions { get; set; }
            public List<Order> Orders { get; set; }
        }

        public void Save()
        {
            var data = new AppData
            {
                Clients = _clients.ToList(),
                Employees = _employees.ToList(),
                Televisions = _televisions.ToList(),
                Orders = _orders.ToList()
            };
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(DataFilePath, json);
        }

        private void Load()
        {
            if (File.Exists(DataFilePath))
            {
                string json = File.ReadAllText(DataFilePath);
                var data = JsonConvert.DeserializeObject<AppData>(json);

                foreach (var client in data.Clients ?? new List<Client>())
                {
                    _clients.Add(client);
                }

                foreach (var employee in data.Employees ?? new List<Employee>())
                {
                    _employees.Add(employee);
                }

                foreach (var tv in data.Televisions ?? new List<Television>())
                {
                    _televisions.Add(tv);
                }

                foreach (var order in data.Orders ?? new List<Order>())
                {
                    _orders.Add(order);
                }
            }
        }
    }
}