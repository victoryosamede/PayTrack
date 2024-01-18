using Moq;
using PayTrackApplication.Application;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.ChangePassword;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApi.Test.AuthenticationTests
{
    public class ChangePasswordHandlerTests
    {
        ChangePasswordCommand request = new ChangePasswordCommand
        {
            CurrentPassword = "password",
            NewPassword = "newPassword"
        };

        [Fact]
        public async Task HandleAsync_WhenUserNotFound_ReturnsError()
        {
            //Arrange
            var mockChangePasswordService = new Mock<IUserRepository>();
            mockChangePasswordService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(It.IsAny<User>());
            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(false);
            authManagerMock.Setup(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));
            mockChangePasswordService.Setup(service => service.UpdateEntity(It.IsAny<User>())).ReturnsAsync(new ActionResponse());

            var handler = new ChangePasswordCommandHandler(mockChangePasswordService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType != ResponseType.NotFound);
            Assert.Contains("User is not found", result.ReasonPhrase);

            //Verify interactions
            mockChangePasswordService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
            authManagerMock.Verify(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            authManagerMock.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            mockChangePasswordService.Verify(service => service.UpdateEntity(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Async_ReturnsError_OldPasswordIsIncorrect()
        {
            //Arrange
            var existingUser = new User
            {
                Email = "victory@example.com",
                UserName = "victory",
                Roles = Roles.User,
                PasswordHash = request.CurrentPassword
            };
            var mockChangePasswordService = new Mock<IUserRepository>();
            mockChangePasswordService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(existingUser);
            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(false);
            authManagerMock.Setup(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));
            mockChangePasswordService.Setup(service => service.UpdateEntity(It.IsAny<User>())).ReturnsAsync(new ActionResponse());

            var handler = new ChangePasswordCommandHandler(mockChangePasswordService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.BadRequest);
            Assert.Contains("Current password is invalid", result.ReasonPhrase);

            //Verify interactions
            mockChangePasswordService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
            authManagerMock.Verify(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            authManagerMock.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            mockChangePasswordService.Verify(service => service.UpdateEntity(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_ReturnSuccess_WhenOldPasswordIsCorrect()
        {
            var existingUser = new User
            {
                Email = "victory@example.com",
                UserName = "victory",
                Roles = Roles.User,
                PasswordHash = request.CurrentPassword
            };
            var mockChangePasswordService = new Mock<IUserRepository>();
            mockChangePasswordService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(existingUser);
            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(true);
            authManagerMock.Setup(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));
            mockChangePasswordService.Setup(service => service.UpdateEntity(It.IsAny<User>())).ReturnsAsync(new ActionResponse());

            var handler = new ChangePasswordCommandHandler(mockChangePasswordService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.Success);
            

            //Verify interactions
            mockChangePasswordService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
            authManagerMock.Verify(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            authManagerMock.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            mockChangePasswordService.Verify(service => service.UpdateEntity(It.IsAny<User>()), Times.Once);
        }
    }
}
