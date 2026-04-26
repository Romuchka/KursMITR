using RepairShopIS.Interfaces;
using System;

namespace RepairShopIS.Models
{
    public class Employee : IEmployee
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Specialty { get; set; }
        public DateTime HireDate { get; set; }

        public int RepairedTVs { get; internal set; }
        public int FaultyRepairs { get; internal set; }

        public Employee(string fullName, string address, string phone, string specialty, DateTime hireDate)
        {
            FullName = fullName != null ? fullName.Trim() : "";
            Address = address != null ? address.Trim() : "";
            Phone = phone != null ? phone.Trim() : "";
            Specialty = specialty != null ? specialty.Trim() : "";
            HireDate = hireDate;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}