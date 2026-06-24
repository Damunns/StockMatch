using StockMatch.Components.Pages;
using Xunit;

namespace StockMatch.Tests;

public class HomeModelTests
{
    [Fact]
    public void SelectLocation_defaults_to_empty_string()
    {
        var model = new Home.SelectLocation();

        Assert.Equal(string.Empty, model.SelectedLocationID);
    }
}
