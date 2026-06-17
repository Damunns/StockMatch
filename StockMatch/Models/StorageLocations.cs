using System.ComponentModel.DataAnnotations;

namespace StockMatch.Models
{
    public class StorageLocations
    {
        public string? StorageLocation { get; set; }
        public string? Description { get; set; }
        public string? Plant { get; set; }
        public string? Status { get; set; }

    }
}
