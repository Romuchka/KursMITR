using RepairShopIS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RepairShopIS.Interfaces
{
    public interface IRepairShopSystem
    {
        ReadOnlyObservableCollection<IClient> Clients { get; }
        ReadOnlyObservableCollection<IEmployee> Employees { get; }
        ReadOnlyObservableCollection<ITelevision> Televisions { get; }
        ReadOnlyObservableCollection<IOrder> Orders { get; }

        void AddClient(IClient client);
        void RemoveClient(IClient client);
        void AddEmployee(IEmployee employee);
        void RemoveEmployee(IEmployee employee);
        void AddTelevision(ITelevision tv);
        void RemoveTelevision(ITelevision tv);
        void AddOrder(IOrder order);
        void RemoveOrder(IOrder order);

        IEnumerable<IOrder> GetOrdersInPeriod(DateTime from, DateTime to);
        IEnumerable<IClient> GetRegularClients();
        IEnumerable<Tuple<IEmployee, int, int>> GetEmployeeStatistics(DateTime from, DateTime to);
        void Save();
    }
}