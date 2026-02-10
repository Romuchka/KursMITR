using RepairShopIS.Interfaces;
using RepairShopIS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace RepairShopIS.Services
{
    public class RepairShopSystem : IRepairShopSystem
    {
        private readonly ObservableCollection<IClient> _clients = new ObservableCollection<IClient>();
        private readonly ObservableCollection<IEmployee> _employees = new ObservableCollection<IEmployee>();
        private readonly ObservableCollection<ITelevision> _televisions = new ObservableCollection<ITelevision>();
        private readonly ObservableCollection<IOrder> _orders = new ObservableCollection<IOrder>();

        private const string DataFilePath = "data.json";

        public RepairShopSystem()
        {
            Load();
        }

        public ReadOnlyObservableCollection<IClient> Clients
        {
            get { return new ReadOnlyObservableCollection<IClient>(_clients); }
        }

        public ReadOnlyObservableCollection<IEmployee> Employees
        {
            get { return new ReadOnlyObservableCollection<IEmployee>(_employees); }
        }

        public ReadOnlyObservableCollection<ITelevision> Televisions
        {
            get { return new ReadOnlyObservableCollection<ITelevision>(_televisions); }
        }

        public ReadOnlyObservableCollection<IOrder> Orders
        {
            get { return new ReadOnlyObservableCollection<IOrder>(_orders); }
        }

        public void AddClient(IClient client)
        {
            _clients.Add(client);
        }

        public void RemoveClient(IClient client)
        {
            _clients.Remove(client);
        }

        public void AddEmployee(IEmployee employee)
        {
            _employees.Add(employee);
        }

        public void RemoveEmployee(IEmployee employee)
        {
            _employees.Remove(employee);
        }

        public void AddTelevision(ITelevision tv)
        {
            _televisions.Add(tv);
        }

        public void RemoveTelevision(ITelevision tv)
        {
            _televisions.Remove(tv);
        }

        public void AddOrder(IOrder order)
        {
            _orders.Add(order);
        }

        public void RemoveOrder(IOrder order)
        {
            _orders.Remove(order);
        }

        public IEnumerable<IOrder> GetOrdersInPeriod(DateTime from, DateTime to)
        {
            return _orders.Where(o => o.ReceiptDate >= from && o.ReceiptDate <= to);
        }

        public IEnumerable<IClient> GetRegularClients()
        {
            return _clients.Where(c => c.IsRegular);
        }

        public IEnumerable<Tuple<IEmployee, int, int>> GetEmployeeStatistics(DateTime from, DateTime to)
        {
            return _employees.Select(emp =>
            {
                var relevant = _orders
                    .Where(o => o.Executor == emp && o.IsCompleted && o.IssueDate >= from && o.IssueDate <= to)
                    .ToList();

                return new Tuple<IEmployee, int, int>(emp, relevant.Count, relevant.Count(o => o.IsFaulty));
            });
        }

        private class AppData
        {
            public List<Client> Clients { get; set; }  // Concrete для сериализации
            public List<Employee> Employees { get; set; }
            public List<Television> Televisions { get; set; }
            public List<Order> Orders { get; set; }
        }

        public void Save()
        {
            var data = new AppData
            {
                Clients = _clients.Cast<Client>().ToList(),
                Employees = _employees.Cast<Employee>().ToList(),
                Televisions = _televisions.Cast<Television>().ToList(),
                Orders = _orders.Cast<Order>().ToList()
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