using KebabMaster.Orders.Domain.DTOs;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Interfaces;
using KebabMaster.Orders.Domain.Services;
using KebabMaster.Orders.Domain.Tests.Tools;
using Moq;
using Xunit;

namespace KebabMaster.Orders.Domain.Tests.Services;

public class OrdersServiceTests
{
        [Fact]
        public async Task CreateOrder_ShouldCallRepositoryCreateOrder()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var ordersService = new OrderService(repositoryMock.Object);
            
            Order order = TestData.Order;
    
            // Act
            await ordersService.CreateOrder(order);
    
            // Assert
            repositoryMock.Verify(r => r.CreateOrder(order), Times.Once);
        }
    
        [Fact]
        public async Task GetOrdersAsync_ShouldCallRepositoryGetOrdersAsync()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var ordersService = new OrderService(repositoryMock.Object);

            var filter = new OrderFilter();
    
            // Act
            await ordersService.GetOrdersAsync(filter);
    
            // Assert
            repositoryMock.Verify(r => r.GetOrdersAsync(filter), Times.Once);
        }
    
        [Fact]
        public async Task GetOrderByIdAsync_ShouldCallRepositoryGetOrderById()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var ordersService = new OrderService(repositoryMock.Object);

            // Arrange
            var id = Guid.NewGuid();
    
            // Act
            await ordersService.GetOrderByIdAsync(id);
    
            // Assert
            repositoryMock.Verify(r => r.GetOrderById(id), Times.Once);
        }
    
        [Fact]
        public async Task DeleteOrder_ShouldCallRepositoryDeleteOrder()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var ordersService = new OrderService(repositoryMock.Object);

            var id = Guid.NewGuid();
    
            // Act
            await ordersService.DeleteOrder(id);
    
            // Assert
            repositoryMock.Verify(r => r.DeleteOrder(id), Times.Once);
        }
    
        [Fact]
        public async Task UpdateOrder_ShouldCallRepositoryUpdateOrder()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var ordersService = new OrderService(repositoryMock.Object);

            var order = OrderUpdateModel.Create(Guid.NewGuid(), TestData.Address, TestData.OrderItems);
    
            // Act
            await ordersService.UpdateOrder(order);
    
            // Assert
            repositoryMock.Verify(r => r.UpdateOrder(order), Times.Once);
        }
}