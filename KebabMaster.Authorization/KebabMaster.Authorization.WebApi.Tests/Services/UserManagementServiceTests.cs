using AutoMapper;
using KebabMaster.Authorization.Domain.Entities;
using KebabMaster.Authorization.Domain.Exceptions;
using KebabMaster.Authorization.Domain.Filter;
using KebabMaster.Authorization.Domain.Interfaces;
using KebabMaster.Authorization.Services;
using KebabMaster.Orders.DTOs;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace KebabMaster.Authorization.WebApi.Tests.Services;

public class UserManagementServiceTests
{

        [Fact]
        public async Task CreateUser_ValidModel_CreatesUser()
        {
            // Arrange
            var model = new RegisterModel
            {
                Email = "tesqwet@example.com",
                UserName = "testqweuser",
                Name = "Test",
                Surname = "User",
                Password = "password"
            };
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var applicationLoggerMock = new Mock<IApplicationLogger>();
            var mapperMock = new Mock<IMapper>();
            var userManagementService = new UserManagementService(
                configurationMock.Object,
                userRepositoryMock.Object,
                applicationLoggerMock.Object,
                mapperMock.Object);
            
            var existingUserByEmail = User.Create("test@example.com", "username", "name", "surname");
            var existingUserByUserName = User.Create("te1st@example.com", "usernam1e", "name", "surname");

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(model.Email))
                .Returns(Task.FromResult<User>(null));

            userRepositoryMock.Setup(repo => repo.GetUserByFilter(It.IsAny<UserFilter>()))
                .ReturnsAsync(new List<User>());

            userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            await userManagementService.CreateUser(model);

            // Assert
            userRepositoryMock.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Once);
            applicationLoggerMock.Verify(logger => logger.LogRegistrationStart(model), Times.Once);
            applicationLoggerMock.Verify(logger => logger.LogRegistrationEnd(model), Times.Once);
        }

        [Fact]
        public async Task CreateUser_UserAlreadyExists_ThrowsUserAlreadyExistsException()
        {
            // Arrange
            var model = new RegisterModel
            {
                Email = "test@example.com",
                UserName = "testuser",
                Name = "Test",
                Surname = "User",
                Password = "password"
            };

            var existingUserByEmail = User.Create("test@example.com", "username", "name", "surname");
            var existingUserByUserName =User.Create("te1st@example.com", "usernam1e", "name", "surname");
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var applicationLoggerMock = new Mock<IApplicationLogger>();
            var mapperMock = new Mock<IMapper>();
            var userManagementService = new UserManagementService(
                configurationMock.Object,
                userRepositoryMock.Object,
                applicationLoggerMock.Object,
                mapperMock.Object);

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(model.Email))
                .ReturnsAsync(existingUserByEmail);

            userRepositoryMock.Setup(repo => repo.GetUserByFilter(It.IsAny<UserFilter>()))
                .ReturnsAsync(new List<User> { existingUserByUserName });

            // Act and Assert
            await Assert.ThrowsAsync<UserAlreadyExistsException>(async () =>
            {
                await userManagementService.CreateUser(model);
            });

            userRepositoryMock.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Never);
            applicationLoggerMock.Verify(logger => logger.LogRegistrationStart(model), Times.Once);
            applicationLoggerMock.Verify(logger => logger.LogRegistrationEnd(model), Times.Never);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ThrowsUnauthorizedException()
        {
            // Arrange
            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "password"
            };
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var applicationLoggerMock = new Mock<IApplicationLogger>();
            var mapperMock = new Mock<IMapper>();
            var userManagementService = new UserManagementService(
                configurationMock.Object,
                userRepositoryMock.Object,
                applicationLoggerMock.Object,
                mapperMock.Object);

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(model.Email))
                .ReturnsAsync((User)null);

            // Act and Assert
            await Assert.ThrowsAsync<UnauthorizedException>(async () =>
            {
                await userManagementService.Login(model);
            });

            applicationLoggerMock.Verify(logger => logger.LogLoginStart(model), Times.Once);
            applicationLoggerMock.Verify(logger => logger.LogLoginEnd(model), Times.Never);
        }

        [Fact]
        public async Task DeleteUser_ValidEmail_DeletesUser()
        {
            // Arrange
            var email = "test@example.com";
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var applicationLoggerMock = new Mock<IApplicationLogger>();
            var mapperMock = new Mock<IMapper>();
            var userManagementService = new UserManagementService(
                configurationMock.Object,
                userRepositoryMock.Object,
                applicationLoggerMock.Object,
                mapperMock.Object);

            userRepositoryMock.Setup(repo => repo.DeleteUser(email))
                .Returns(Task.CompletedTask);

            // Act
            await userManagementService.DeleteUser(email);

            // Assert
            userRepositoryMock.Verify(repo => repo.DeleteUser(email), Times.Once);
            applicationLoggerMock.Verify(logger => logger.LogDeleteStart(email), Times.Once);
            applicationLoggerMock.Verify(logger => logger.LogDeleteEnd(email), Times.Once);
        }
}