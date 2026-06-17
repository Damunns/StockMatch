namespace StockMatch.Models
{
    public class WIP
    {
        public required string WorkOrder { get; set; }
        public string? Material { get; set; }
        public int? Quantity { get; set; }
        public string? Plant { get; set; }
        public string? StorageLocation {  get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? StartDate { get; set; }
        public string? ProfitCenter { get; set; }

    }
}
