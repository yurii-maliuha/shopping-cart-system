using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class CartTests
{
    private Fixture _fixture;

    public CartTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void AddItemForInvalidQuantityThrowsException()
    {
        // Arrange
        var invalidQuantity = 0;
        var product = _fixture.Create<Product>();
        var cart = _fixture.Create<Cart>();

        // Act
        Action action = () => cart.AddItem(product, invalidQuantity);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AddNewItem()
    {
        //Arrange

        // Act

        // Assert
    }

    [Fact]
    public void AddExistingItem()
    {
        //Arrange

        // Act

        // Assert
    }

    [Fact]
    public void RemoveUnexistingItemThrowsException()
    {
        //Arrange

        // Act

        // Assert
    }

    [Fact]
    public void RemoveItem()
    {
        //Arrange

        // Act

        // Assert
    }

}
