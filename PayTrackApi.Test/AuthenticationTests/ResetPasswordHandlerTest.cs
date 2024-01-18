using Moq;
using PayTrackApplication.Application;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.ResetPassword;
using PayTrackApplication.Application.UserServices.VerifyResetToken;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApi.Test.AuthenticationTests
{
    public  class ResetPasswordHandlerTest
    {
        private readonly ResetPasswordCommand request = new ResetPasswordCommand
        { 
            Token = "ResetToken",
            Email = "victory@example.com",
            NewPassword = Guid.NewGuid().ToString()
        };

        User existingUser = new User
        {
            Email = "victory@example.com",
            UserName = "victory",
            Roles = Roles.User
        };

        [Fact]

        public async void HandleAsync_ReturnsError_WhenUserNotFound()
        {
            //Arrange
            var mockResetPasswordService = new Mock<IUserRepository>();
            mockResetPasswordService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);

            var authManagerService = new Mock<IAuthenticationManager>();
            authManagerService.Setup(service => service.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            var handler = new ResetPasswordCommandHandler(mockResetPasswordService.Object, authManagerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("User Not Found ", result.ReasonPhrase);

            //Verify interaction
            mockResetPasswordService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
            authManagerService.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
            
        }

        [Fact]

        public async void HandleAsync_ReturnsError_WhenResetTokenDoesNotMatch()
        {
            //Arrange
            existingUser.PasswordResetToken = "Resett Token";

            var mockResetPasswordService = new Mock<IUserRepository>();
            mockResetPasswordService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

            var authManagerService = new Mock<IAuthenticationManager>();
            authManagerService.Setup(service => service.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            var handler = new ResetPasswordCommandHandler(mockResetPasswordService.Object, authManagerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("Invalid Token", result.ReasonPhrase);

            //Verify interactions
            mockResetPasswordService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
            authManagerService.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never());

        }

        [Fact]

        public async void HandleAsync_ReturnsError_WhenSessionExpired()
        {
            //Arrange
            existingUser.PasswordResetToken = "ResetToken";
            existingUser.ResetTokenExpires = DateTime.UtcNow.AddMinutes(-16);

            var mockResetPasswordService = new Mock<IUserRepository>();
            mockResetPasswordService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

            var authManagerService = new Mock<IAuthenticationManager>();
            authManagerService.Setup(service => service.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            var handler = new ResetPasswordCommandHandler(mockResetPasswordService.Object, authManagerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("Session has expired", result.ReasonPhrase);

            //Verify interactions
            mockResetPasswordService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
            authManagerService.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never());


        }

        [Fact]
        public async void HandleAsync_ReturnsSuccess_WhenCommandMeetsAllConditions()
        {
            //Arrange
            existingUser.PasswordResetToken = "ResetToken";
            existingUser.ResetTokenExpires = DateTime.UtcNow.AddMinutes(14);

            var mockResetPasswordService = new Mock<IUserRepository>();
            mockResetPasswordService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);
           
            mockResetPasswordService.Setup(service => service.UpdateEntity(It.IsAny<User>())).ReturnsAsync(new ActionResponse());

            var authManagerService = new Mock<IAuthenticationManager>();
            authManagerService.Setup(service => service.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            var handler = new ResetPasswordCommandHandler(mockResetPasswordService.Object, authManagerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.Success);
            Assert.Empty(result.ReasonPhrase);

            //Verify interactions
            mockResetPasswordService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
            authManagerService.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once());
            mockResetPasswordService.Verify(service => service.UpdateEntity(It.IsAny<User>()), Times.Once());


        }
    }
}
