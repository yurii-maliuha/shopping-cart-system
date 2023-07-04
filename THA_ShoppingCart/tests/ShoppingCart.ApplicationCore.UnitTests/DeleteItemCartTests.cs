using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class DeleteItemCartTests
{
    private Fixture _fixture;

    public DeleteItemCartTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void DeleteIgnoreUnexistingItem()
    {
        //Arrange
        var cartItems = _fixture.CreateMany<CartItem>().ToList();
        var itemsCount = cartItems.Count;
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);
        var product = _fixture.Create<Product>();

        // Act
        cart.DeleteItem(product);

        // Assert
        cart.Items.Count.Should().Be(itemsCount);

    }

    [Fact]
    public void DeleteItem()
    {
        //Arrange
        var cartItems = _fixture.CreateMany<CartItem>().ToList();
        var product = _fixture.Create<Product>();
        cartItems.Add(new CartItem(
            id: Guid.NewGuid(),
            productId: product.Id,
            unitPrice: product.Price,
            quantity: 2));

        var itemsCount = cartItems.Count;
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);

        // Act
        cart.DeleteItem(product);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount - 1);
        cart.Items.Any(x => x.ProductId == product.Id).Should().BeFalse();
    }
}
