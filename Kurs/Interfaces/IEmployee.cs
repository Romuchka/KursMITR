using System;

namespace RepairShopIS.Interfaces
{
    public interface IEmployee
    {
        string FullName { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string Specialty { get; set; }
        DateTime HireDate { get; set; }
        int RepairedTVs { get; }
        int FaultyRepairs { get; }
    }
}