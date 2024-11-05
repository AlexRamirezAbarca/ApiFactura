namespace ApiFactura.Models
{
    public class DetailBill
    {
        public int IdDetailBill { get; set; }
        public int IdBill { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Amount * UnitPrice;
    }
}
