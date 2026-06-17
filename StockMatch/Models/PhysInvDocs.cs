namespace StockMatch.Models
{
    public class PhysInvDocs
    {
        public required string PhysInvDoc { get; set; }
        public string? PhysInvDocItem { get; set; }
        public string? Material { get; set; }
        public string? Batch { get; set; }
        public string? BookQty {  get; set; }
        public string? StorageLocation { get; set; }
        public string? StockStatus { get; set; }
        public string? CountStatus { get; set; }
        public string? CountDateTime { get; set; }
        public string? Reference { get; set; }
        public string? UploadTime { get; set; }
        public string? UploadUser { get; set; }

    }
}
