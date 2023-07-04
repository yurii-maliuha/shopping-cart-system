using AutoFixture;
using FluentAssertions;
using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Exceptions;
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
        action.Should().Throw<InvalidQuantityValueOfCartItemsDomainException>();
    }

    [Fact]
    public void AddNewItem()
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
        cart.AddItem(product);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount + 1);
        cart.Items.Count(x => x.ProductId == product.Id).Should().Be(1);
    }

    [Fact]
    public void AddExistingItem()
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
        cart.AddItem(product);

        // Assert
        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().Be(itemsCount);
        cart.Items.First(x => x.ProductId == product.Id).Quantity.Should().Be(2);
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
