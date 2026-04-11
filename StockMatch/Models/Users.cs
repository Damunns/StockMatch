namespace StockMatch.Models
{
    public class Users
    {
        public required string EmailUsername { get; set; }
        public string? LogonUsername { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Access {  get; set; }

    }
}
