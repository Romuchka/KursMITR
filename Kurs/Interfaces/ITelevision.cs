namespace RepairShopIS.Interfaces
{
    public interface ITelevision
    {
        string EquipmentType { get; set; }
        string Brand { get; set; }
        string Country { get; set; }
        string Manufacturer { get; set; }
        string PhotoLink { get; set; }
        int UsageYears { get; set; }
    }
}