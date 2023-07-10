using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Exceptions;
using ShoppingCart.ApplicationCore.UnitTests.Shared;
using Xunit;

namespace ShoppingCart.ApplicationCore.UnitTests;

public class RemoveItemShoppingCartTests
{
    [Fact]
    public void RemoveUnexistingItemThrowsException()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new ShoppingCartItemCreator());
        var cart = new Entities.ShoppingCart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);
        var product = Fixture.Create(new ProductCreator());

        // Act
        Action action = () => cart.RemoveItem(product);

        // Assert
        action.Should().Throw<NotFoundCartItemOnUpdateDomainException>();
    }

    [Fact]
    public void RemoveItemsUpdateQuantity()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new ShoppingCartItemCreator());
        var product = Fixture.Create(new ProductCreator());
        cartItems.Add(new ShoppingCartItem(
            id: Guid.NewGuid(),
            productId: product.Id,
            unitPrice: product.Price,
            quantity: 3));

        var itemsCount = cartItems.Count;
        var cart = new Entities.ShoppingCart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);
        var cartTotalPrice = cart.Total;

        // Act
        cart.RemoveItem(product, quantity: 1);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount);
        cart.Items.First(x => x.ProductId == product.Id).Quantity.Should().Be(2);
        cart.Total.Should().Be(cartTotalPrice - product.Price);


    }

    [Fact]
    public void RemoveItemCauseToItemDeletion()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new ShoppingCartItemCreator());
        var product = Fixture.Create(new ProductCreator());
        cartItems.Add(new ShoppingCartItem(
            id: Guid.NewGuid(),
            productId: product.Id,
            unitPrice: product.Price,
            quantity: 1));

        var itemsCount = cartItems.Count;
        var cart = new Entities.ShoppingCart(
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
