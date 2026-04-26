using RepairShopIS.Interfaces;

namespace RepairShopIS.Models
{
    public class Television : ITelevision
    {
        public string EquipmentType { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public string PhotoLink { get; set; }
        public int UsageYears { get; set; }

        public Television(string equipmentType, string brand, string country, string manufacturer, string photoLink, int usageYears)
        {
            EquipmentType = equipmentType != null ? equipmentType.Trim() : "Телевизор";
            Brand = brand != null ? brand.Trim() : "";
            Country = country != null ? country.Trim() : "";
            Manufacturer = manufacturer != null ? manufacturer.Trim() : "";
            PhotoLink = photoLink != null ? photoLink.Trim() : null;
            UsageYears = usageYears;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} ({2})", EquipmentType, Brand, Country);
        }
    }
}