namespace RepairShopIS.Interfaces
{
    public interface IEmployee
    {
        string FullName { get; set; }
        int Age { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string Specialty { get; set; }
        int ExperienceYears { get; set; }
        int RepairedTVs { get; }
        int FaultyRepairs { get; }
    }
}