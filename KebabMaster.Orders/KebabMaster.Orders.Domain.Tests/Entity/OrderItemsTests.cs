﻿using KebabMaster.Orders.Domain.Entities;
using Xunit;

namespace KebabMaster.Orders.Domain.Tests.EntityTesting;

public class OrderItemsTests
{
    [Fact]
    public void Create_ValidOrderItem_ReturnsOrderItemInstance()
    {
        // Arrange
        Guid menuItemId = Guid.NewGuid();
        int quantity = 2;

        // Act
        OrderItem orderItem = OrderItem.Create(menuItemId, quantity);

        // Assert
        Assert.NotNull(orderItem);
        Assert.Equal(menuItemId, orderItem.MenuItemId);
        Assert.Equal(quantity, orderItem.Quantity);
    }

    [Fact]
    public void Create_InvalidMenuItemId_ThrowsException()
    {
        // Arrange
        Guid menuItemId = default;
        int quantity = 2;

        // Act & Assert
        Assert.Throws<Exception>(() => OrderItem.Create(menuItemId, quantity));
    }

    [Fact]
    public void Create_InvalidQuantity_ThrowsException()
    {
        // Arrange
        Guid menuItemId = Guid.NewGuid();
        int quantity = 0;

        // Act & Assert
        Assert.Throws<Exception>(() => OrderItem.Create(menuItemId, quantity));
    }
}