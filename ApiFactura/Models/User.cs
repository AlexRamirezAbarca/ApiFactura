namespace ApiFactura.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Identification { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOrigin { get; set; }
        public bool IsAdmin { get; set; }
        public string NameUser {  get; set; }
        public string Email { get; set; }


    }
}
