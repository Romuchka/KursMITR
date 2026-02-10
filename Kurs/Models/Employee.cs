using RepairShopIS.Interfaces;

namespace RepairShopIS.Models
{
    public class Employee : IEmployee
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Specialty { get; set; }
        public int ExperienceYears { get; set; }

        public int RepairedTVs { get; internal set; }
        public int FaultyRepairs { get; internal set; }

        public Employee(string fullName, int age, string address, string phone, string specialty, int experienceYears)
        {
            FullName = fullName != null ? fullName.Trim() : "";
            Age = age;
            Address = address != null ? address.Trim() : "";
            Phone = phone != null ? phone.Trim() : "";
            Specialty = specialty != null ? specialty.Trim() : "";
            ExperienceYears = experienceYears;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}