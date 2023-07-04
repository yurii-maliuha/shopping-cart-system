using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Exceptions;
using ShoppingCart.ApplicationCore.UnitTests.Shared;
using Xunit;


namespace ShoppingCart.ApplicationCore.UnitTests;

public class AddItemCartTests
{
    [Fact]
    public void AddItemForInvalidQuantityThrowsException()
    {
        // Arrange
        var invalidQuantity = 0;
        var product = Fixture.Create(new ProductCreator());
        var cart = Fixture.Create(new CartCreator());

        // Act
        Action action = () => cart.AddItem(product, invalidQuantity);

        // Assert
        action.Should().Throw<InvalidQuantityValueOfCartItemsDomainException>();
    }

    [Fact]
    public void AddItemForANewItem()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new CartItemCreator());
        var itemsCount = cartItems.Count;
        var cart = new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: cartItems);
        var cartTotalPrice = cart.Total;
        var product = Fixture.Create(new ProductCreator());

        // Act
        cart.AddItem(product);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount + 1);
        cart.Items.Count(x => x.ProductId == product.Id).Should().Be(1);
        cart.Total.Should().Be(cartTotalPrice + product.Price);
    }

    [Fact]
    public void AddItemForTheExistingItem()
    {
        //Arrange
        var cartItems = Fixture.CreateMany(3, new CartItemCreator());
        var product = Fixture.Create(new ProductCreator());
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
        var cartTotalPrice = cart.Total;

        // Act
        cart.AddItem(product);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount);
        cart.Items.First(x => x.ProductId == product.Id).Quantity.Should().Be(2);
        cart.Total.Should().Be(cartTotalPrice + product.Price);
    }
}
