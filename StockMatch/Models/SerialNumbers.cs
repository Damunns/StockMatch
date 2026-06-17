namespace StockMatch.Models
{
    public class SerialNumbers
    {
        public required string SerialNumber { get; set; }
        public string? Material { get; set; }
        public string? Batch { get; set; }
        public string? StorageLocation { get; set; }

    }
}
