namespace ApiFactura.Models
{
    public class Bill
    {
        public int IdBill { get; set; }
        public int IdClient { get; set; }
        public int IdSeller { get; set; }
        public DateTime Date { get; set; }
        public string Payment { get; set; }
        public List<DetailBill> Details { get; set; }
    }
}
