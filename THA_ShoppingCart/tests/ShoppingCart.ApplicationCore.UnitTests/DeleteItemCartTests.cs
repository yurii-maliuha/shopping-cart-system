using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.UnitTests.Shared;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class DeleteItemCartTests
{
    [Fact]
    public void DeleteIgnoreUnexistingItem()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new CartItemCreator());
        var itemsCount = cartItems.Count;
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);
        var product = Fixture.Create(new ProductCreator());

        // Act
        cart.DeleteItem(product);

        // Assert
        cart.Items.Count.Should().Be(itemsCount);

    }

    [Fact]
    public void DeleteItem()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new CartItemCreator());
        var product = Fixture.Create(new ProductCreator());
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
        var cartTotalPrice = cart.Total;

        // Act
        cart.DeleteItem(product);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount - 1);
        cart.Items.Any(x => x.ProductId == product.Id).Should().BeFalse();
        cart.Total.Should().Be(cartTotalPrice - (product.Price * 2));
    }
}
