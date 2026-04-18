using System.ComponentModel.DataAnnotations;

namespace StockMatch.Models
{
    public class Count
    {
        [Required(ErrorMessage = "StorageLocation is required")]
        public string StorageLocation { get; set; }

        [Required(ErrorMessage = "Material is required")]
        public string Material { get; set; }
        public string? Batch { get; set; }
        public string? Serial { get; set; }

        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10,000")]
        public int Quantity { get; set; } = 1;

        public DateTime LastChecked { get; set; } = DateTime.Now;
    }
}
