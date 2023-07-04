using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.ValueObjects;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class CartItemTests
{
    private Fixture _fixture;
    public CartItemTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void CreateBasedOnProductAndQuantity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var product = _fixture.Create<Product>();
        var quantity = 2;

        // Act
        var cartItem = CartItem.Create(id, product, quantity);

        // Assert
        cartItem.Id.Should().Be(id);
    }

    [Fact]
    public void AddQuantity()
    {
        // Arrange
        var cartItem = new CartItem(
            id: Guid.NewGuid(),
            productId: Guid.NewGuid(),
            unitPrice: new Money(14, CurrencyCode.USD),
            quantity: 2);

        // Act
        cartItem.AddQuantity(1);

        // Assert
        cartItem.Quantity.Should().Be(3);
    }

    [Fact]
    public void SetQuantity()
    {
        // Arrange
        var cartItem = new CartItem(
            id: Guid.NewGuid(),
            productId: Guid.NewGuid(),
            unitPrice: new Money(14, CurrencyCode.USD),
            quantity: 1);

        // Act
        cartItem.SetQuantity(10);

        // Assert
        cartItem.Quantity.Should().Be(10);
    }

}
