using System.ComponentModel.DataAnnotations;

namespace StockMatch.Models
{
    public class InventoryCount
    {
        public int CountID { get; set; }

        [Required(ErrorMessage = "Material is required")]
        public string? Material { get; set; }

        [Required(ErrorMessage = "Batch is required")]
        public string? Batch { get; set; }
        public string? SerialNumber { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int CountQty { get; set; }
        public string? Plant { get; set; }

        [Required(ErrorMessage = "StorageLocation is required")]
        public string? StorageLocation { get; set; }
        public string? Reference { get; set; }
        public string? PhysInvDoc { get; set; }
        public string? PhysInvDocItem { get; set; }
        public string? CountUser { get; set; }
        public DateTime CountDateTime { get; set; }
    }
}
