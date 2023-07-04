using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.ValueTypes;
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
        var product = _fixture.Create<Product>();
        var quantity = 2;

        // Act
        var cartItem = CartItem.Create(product, quantity);

        // Assert
        cartItem.Should().NotBeNull();
        cartItem.Quantity.Should().Be(quantity);
        cartItem.ProductId.Should().Be(product.Id);
        cartItem.UnitPrice.Should().Be(product.Price);
    }

    [Fact]
    public void AddQuantity()
    {
        // Arrange
        var cartItem = new CartItem(
            productId: Guid.NewGuid(),
            unitPrice: new Money(14, "USD"),
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
            productId: Guid.NewGuid(),
            unitPrice: new Money(14, "USD"),
            quantity: 1);

        // Act
        cartItem.SetQuantity(10);

        // Assert
        cartItem.Quantity.Should().Be(10);
    }

}
