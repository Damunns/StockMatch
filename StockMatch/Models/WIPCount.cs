namespace StockMatch.Models
{
    public class WIPCount
    {
        public required string CountID { get; set; }
        public string? WorkOrder { get; set; }
        public string? SerialNumber { get; set; }
        public string? CountQty { get; set; }
        public string? Plant {  get; set; }
        public string? StorageLocation { get; set; }
        public string? Reference { get; set; }
        public string? CountUser { get; set; }
        public DateTime CountDateTime { get; set; }
        public string? CountStatus { get; set; }

    }
}
