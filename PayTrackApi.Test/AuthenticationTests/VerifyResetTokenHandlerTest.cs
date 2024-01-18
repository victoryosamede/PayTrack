using Moq;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application;
using PayTrackApplication.Application.UserServices.ForgotPassword;
using PayTrackApplication.Application.UserServices.VerifyResetToken;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;
using System.Linq.Expressions;
using System.Diagnostics;

namespace PayTrackApi.Test.AuthenticationTests
{
    public class VerifyResetTokenHandlerTest
    {
        VerifyResetTokenCommand request = new VerifyResetTokenCommand
        {
            Email = "victory@example.com",
            Token = "ResetToken"
        };

        User existingUser = new User
        {
            Email = "victory@example.com",
            UserName = "victory",
            Roles = Roles.User,
            
        };

        [Fact]
        public async Task HandleAsync_WhenUserNotFound_ReturnsError()
        {
            //Arrange
            var mockVerifyResetTokenService = new Mock<IUserRepository>();
            mockVerifyResetTokenService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);



            var handler = new VerifyResetTokenCommandHandler(mockVerifyResetTokenService.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.NotFound);
            Assert.Equal(ResponseType.NotFound, result.ResponseType);

            //Verify interactions
            mockVerifyResetTokenService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);

        }

        [Fact]
         
        public async Task HandleAsync_ReturnsError_WhenResetTokenIsExpired()
        {
            //Arrange
            existingUser.ResetTokenExpires = DateTime.UtcNow.AddMinutes(-15);

            var mockVerifyResetTokenService = new Mock<IUserRepository>();
            mockVerifyResetTokenService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

            //Act

            var handler = new VerifyResetTokenCommandHandler(mockVerifyResetTokenService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("Invalid Token", result.ReasonPhrase);
            mockVerifyResetTokenService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);


        }
        [Fact]

        public async void HandleAsync_ReturnsIncorrectPin_WhenPinIsNotCorrect()
        {
            //Arrange
            existingUser.ResetTokenExpires = DateTime.UtcNow.AddMinutes(-14);
            existingUser.PasswordResetToken = "Resett Token";

            var mockVerifyResetTokenService = new Mock<IUserRepository>();
            mockVerifyResetTokenService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);
            
            //Act
            var handler = new VerifyResetTokenCommandHandler(mockVerifyResetTokenService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ResponseType == ResponseType.Success);
            Assert.Contains("Invalid Token", result.ReasonPhrase);
            mockVerifyResetTokenService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
            
        }

        [Fact]

        public async void HandleAsync_ReturnSuccess_WhenTokenAndCreationTimeAreCorrect()
        {
            //Arrange
            existingUser.ResetTokenExpires = DateTime.UtcNow.AddMinutes(5);
            existingUser.PasswordResetToken = "ResetToken";

            var mockVerifyResetTokenService = new Mock<IUserRepository>();
            mockVerifyResetTokenService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);
            mockVerifyResetTokenService.Setup(service => service.UpdateEntity(It.IsAny<User>())).ReturnsAsync(new ActionResponse());

            //Act
            var handler = new VerifyResetTokenCommandHandler(mockVerifyResetTokenService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            //Assert.NotNull(result);
            //Assert.True(result.ResponseType == ResponseType.Success);
            //Assert.Null(result.ReasonPhrase);
            //Assert
            Assert.NotNull(result);
            Debug.WriteLine($"Actual ResponseType: {result.ResponseType}");
            Assert.True(result.ResponseType == ResponseType.Success);
            Assert.Empty(result.ReasonPhrase);


            mockVerifyResetTokenService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            mockVerifyResetTokenService.Verify(service => service.UpdateEntity(It.IsAny<User>()), Times.Once);

        }

        
    }
}
