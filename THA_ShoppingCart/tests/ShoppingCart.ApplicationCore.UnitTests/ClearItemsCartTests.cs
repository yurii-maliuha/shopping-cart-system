using FluentAssertions;
using ShoppingCart.ApplicationCore.UnitTests.Shared;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class ClearItemsCartTests
{
    [Fact]
    public void ClearCart()
    {
        //Arrange
        var cart = Fixture.Create(new CartCreator());

        // Act
        cart.Clear();

        // Arrange
        cart.Items.Count.Should().Be(0);
    }


}
