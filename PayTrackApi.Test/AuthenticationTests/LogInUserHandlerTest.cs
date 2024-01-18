using Microsoft.Identity.Client;
using Moq;
using PayTrackApplication.Application;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.LoginUser;
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
    public class LogInUserHandlerTest
    {
        LoginUserCommand request = new LoginUserCommand
        {
            Email = "victory@example.com",
            UserName = "victory",
            Password = "password"
        };
        User existingUser = new User
        {
            Email = "victory@example.com",
            UserName = "victory",
            Roles = Roles.User
        };
        private readonly TokenModel tokenModel = new TokenModel
        {
            AccessToken = new Guid().ToString(),
            RefreshToken = Guid.NewGuid().ToString(),
            
        };
        
        [Fact]
        public async void HandleAsync_ReturnsError_WhenEmailNotFound()
        {
            //Arrange

            var mockLoginUserService = new Mock<IUserRepository>();
            mockLoginUserService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.GenerateToken(It.IsAny<User>()));

            var handler = new LoginUserCommandHandler(mockLoginUserService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("Email Or Password Is Invalid", result.ReasonPhrase);

            //Verify interactions
            mockLoginUserService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            authManagerMock.Verify(manager => manager.GenerateToken(It.IsAny<User>()), Times.Never);


        }

        [Fact]

        public async void HandleAsync_ReturnsError_WhenPasswordIsInvalid()
        {
            //Arrange
            var mockLoginUserService = new Mock<IUserRepository>();
            mockLoginUserService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);
            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(false);
            authManagerMock.Setup(manager => manager.GenerateToken(It.IsAny<User>())).Returns(tokenModel);

            var handler = new LoginUserCommandHandler(mockLoginUserService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("Email Or Password Is Invalid", result.ReasonPhrase);

            //Verify interactions
            mockLoginUserService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            authManagerMock.Verify(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            authManagerMock.Verify(manager => manager.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]

        public async void HandleAsync_ReturnsToken_WhenEmailAndPasswordIsValid()
        {
            //Arrange
            var mockLogInUserService = new Mock<IUserRepository>();
            mockLogInUserService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);
            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(service => service.VerifyPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(true);
            authManagerMock.Setup(service => service.GenerateToken(It.IsAny<User>())).Returns(tokenModel);

            var handler = new LoginUserCommandHandler(mockLogInUserService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.Success);
            var payload = result.PayLoad as TokenModel;
            Assert.Equal(tokenModel.AccessToken, payload!.AccessToken);
            Assert.Equal(tokenModel.RefreshToken, payload.RefreshToken);

            //Verify interactions
            mockLogInUserService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            authManagerMock.Verify(manager => manager.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            authManagerMock.Verify(manager => manager.GenerateToken(It.IsAny<User>()), Times.Once);
        }
    }
}
