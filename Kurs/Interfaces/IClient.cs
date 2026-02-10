namespace RepairShopIS.Interfaces
{
    public interface IClient
    {
        string FullName { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string DiscountCardNumber { get; set; }
        bool IsRegular { get; }
    }
}