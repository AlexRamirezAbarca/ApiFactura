namespace ApiFactura.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public int CodeProduct { get; set; }
        public string NameProduct { get; set; }
        public decimal UnitPrice { get; set; }
        public bool StatusProduct { get; set; }
    }
}
