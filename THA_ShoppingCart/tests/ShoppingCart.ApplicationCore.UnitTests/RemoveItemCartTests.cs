using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Exceptions;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class RemoveItemCartTests
{
    private Fixture _fixture;

    public RemoveItemCartTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void RemoveUnexistingItemThrowsException()
    {
        //Arrange
        var cartItems = _fixture.CreateMany<CartItem>().ToList();
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);
        var product = _fixture.Create<Product>();

        // Act
        Action action = () => cart.RemoveItem(product);

        // Assert
        action.Should().Throw<NotFoundCartItemOnUpdateDomainException>();
    }

    [Fact]
    public void RemoveItemsUpdateQuantity()
    {
        //Arrange
        var cartItems = _fixture.CreateMany<CartItem>().ToList();
        var product = _fixture.Create<Product>();
        cartItems.Add(new CartItem(
            id: Guid.NewGuid(),
            productId: product.Id,
            unitPrice: product.Price,
            quantity: 3));

        var itemsCount = cartItems.Count;
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);

        // Act
        cart.RemoveItem(product, quantity: 1);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount);
        cart.Items.First(x => x.ProductId == product.Id).Quantity.Should().Be(2);

    }

    [Fact]
    public void RemoveItemCauseToItemDeletion()
    {
        //Arrange
        var cartItems = _fixture.CreateMany<CartItem>().ToList();
        var product = _fixture.Create<Product>();
        cartItems.Add(new CartItem(
            id: Guid.NewGuid(),
            productId: product.Id,
            unitPrice: product.Price,
            quantity: 1));

        var itemsCount = cartItems.Count;
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);

        // Act
        cart.RemoveItem(product, quantity: 1);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount - 1);
        cart.Items.Any(x => x.ProductId == product.Id).Should().BeFalse();
    }
}
