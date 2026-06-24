using System.ComponentModel.DataAnnotations;
using StockMatch.Models;
using Xunit;

namespace StockMatch.Tests;

public class InventoryCountValidationTests
{
    [Fact]
    public void InventoryCount_requires_basic_fields()
    {
        var model = new InventoryCount();

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);

        Assert.False(isValid);
        Assert.Contains(results, result => result.ErrorMessage == "Material is required");
        Assert.Contains(results, result => result.ErrorMessage == "Batch is required");
        Assert.Contains(results, result => result.ErrorMessage == "StorageLocation is required");
    }

    [Fact]
    public void InventoryCount_is_valid_when_required_values_are_present()
    {
        var model = new InventoryCount
        {
            Material = "MAT-001",
            Batch = "BATCH-001",
            StorageLocation = "LOC-001",
            CountQty = 1
        };

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);

        Assert.True(isValid);
        Assert.Empty(results);
    }
}
