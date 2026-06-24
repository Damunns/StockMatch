using Microsoft.AspNetCore.Components;
using StockMatch.Components.Pages;
using Xunit;

namespace StockMatch.Tests;

public class RouteTests
{
    [Theory]
    [InlineData(typeof(Home), "/")]
    [InlineData(typeof(CountInventory), "/countinventory")]
    [InlineData(typeof(CountWIP), "/countwip")]
    public void Components_expose_expected_routes(Type componentType, string expectedRoute)
    {
        var routes = componentType
            .GetCustomAttributes(typeof(RouteAttribute), inherit: true)
            .Cast<RouteAttribute>()
            .Select(attribute => attribute.Template);

        Assert.Contains(expectedRoute, routes);
    }
}
