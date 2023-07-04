using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class ClearItemsCartTests
{
    private Fixture _fixture;

    public ClearItemsCartTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ClearCart()
    {
        //Arrange
        var cart = _fixture.Create<Cart>();

        // Act
        cart.Clear();

        // Arrange
        cart.Items.Count.Should().Be(0);
    }


}
