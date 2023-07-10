using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.ValueObjects;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class CalculateTotalValueOfShoppingCartTests
{
    [Fact]
    public void CalculateTotalValue()
    {
        // Arrange
        var cartItems = new List<ShoppingCartItem>()
        {
            new ShoppingCartItem(
                id: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                unitPrice: new Money(14, CurrencyCode.USD),
                quantity: 1),
            new ShoppingCartItem(
                id: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                unitPrice: new Money(8, CurrencyCode.USD),
                quantity: 2),
            new ShoppingCartItem(
                id: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                unitPrice: new Money(10, CurrencyCode.USD),
                quantity: 1)
        };
        var cart = new Entities.ShoppingCart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);

        // Act & Arrange
        cart.Total.Should().Be(new Money(40, CurrencyCode.USD));
    }
}
