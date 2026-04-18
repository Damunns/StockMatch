namespace StockMatch.Models
{
    public class Materials
    {
        public required string Material { get; set; }
        public string? Description { get; set; }
        public string? ProdProfile { get; set; }
        public string? SerialProfile { get; set; }
        public string? BatchRequired { get; set; }
        public string? ProfitCenter { get; set; }
    }
}
