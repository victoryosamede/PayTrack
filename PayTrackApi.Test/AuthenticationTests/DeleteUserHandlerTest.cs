using Moq;
using PayTrackApplication.Application;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.DeleteUser;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApi.Test.AuthenticationTests
{
    public class DeleteUserHandlerTest
    {
        DeleteUserCommand request = new DeleteUserCommand() { Id = 1 };

        [Fact]
        public async void HandleAsync_ReturnsError_WhenUserNotFound()
        {
            //Arrange
            var mockDeleteUserService = new Mock<IUserRepository>();
            mockDeleteUserService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(It.IsAny<User>());



            var handler = new DeleteUserCommandHandler(mockDeleteUserService.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("User is not found.", result.ReasonPhrase);

            //Verify interactions
            mockDeleteUserService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
            mockDeleteUserService.Verify(service => service.DeleteEntity(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async void HandleAsync_ReturnsError_WhenDeleteIsNotSuccessful()
        {
            //Arrange
            var existingUser = new User
            {
                Id = 1,
                Email = "victory@example.com",
                UserName = "victory",
                Roles = Roles.User
            };
            var mockDeleteUserService = new Mock<IUserRepository>();
            mockDeleteUserService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(existingUser);

            var failedActionResponse = new ActionResponse();
            failedActionResponse.AddErrors("Error");
            mockDeleteUserService.Setup(service => service.DeleteEntity(It.IsAny<User>())).ReturnsAsync(failedActionResponse);

            var handler = new DeleteUserCommandHandler(mockDeleteUserService.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType != ResponseType.Success);

            //Verify interactions
            mockDeleteUserService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
            mockDeleteUserService.Verify(service => service.DeleteEntity(It.IsAny<User>()), Times.Once);
        }

        [Fact]

        public async void Handle_ReturnSuccess_WhenUserIsFoundAndDeleteIsSuccessful()
        {
            //Arrange
            var existingUser = new User
            {
                Id = 1,
                Email = "victory@example.com",
                UserName = "victory",
                Roles = Roles.User
            };
            var mockDeleteUserService = new Mock<IUserRepository>();
            mockDeleteUserService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(existingUser);
            mockDeleteUserService.Setup(service => service.DeleteEntity(It.IsAny<User>())).ReturnsAsync(new ActionResponse());

            var handler = new DeleteUserCommandHandler(mockDeleteUserService.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.Success);

            //Verify interactions
            mockDeleteUserService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
            mockDeleteUserService.Verify(service => service.DeleteEntity(It.IsAny<User>()), Times.Once);
        }
    }
}
