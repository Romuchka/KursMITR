using RepairShopIS.Interfaces;

namespace RepairShopIS.Models
{
    public class Client : IClient
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DiscountCardNumber { get; set; }

        public Client(string fullName, string address, string phone, string discountCardNumber = null)
        {
            FullName = fullName != null ? fullName.Trim() : "";
            Address = address != null ? address.Trim() : "";
            Phone = phone != null ? phone.Trim() : "";
            DiscountCardNumber = discountCardNumber != null ? discountCardNumber.Trim() : null;
        }

        public bool IsRegular
        {
            get { return !string.IsNullOrWhiteSpace(DiscountCardNumber); }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}