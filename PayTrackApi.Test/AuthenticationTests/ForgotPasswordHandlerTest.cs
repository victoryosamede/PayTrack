using Moq;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.ChangePassword;
using PayTrackApplication.Application;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayTrackApplication.Application.UserServices.ForgotPassword;
using Microsoft.AspNetCore.Mvc.Formatters;
using static PayTrackApplication.Domain.Constants.Enums;
using System.Linq.Expressions;

namespace PayTrackApi.Test.AuthenticationTests
{
    public class ForgotPasswordHandlerTest
    {
        ForgotPasswordCommand request = new ForgotPasswordCommand
        {
            Email = "victory@example.com"
           
        };

        User existingUser = new User
        {
            Email = "victory@example.com",
            UserName = "victory",
            Roles = Roles.User
          
        };

        [Fact]
        public async Task HandleAsync_WhenUserNotFound_ReturnsError()
        {
            //Arrange
            var mockForgotPasswordService = new Mock<IUserRepository>();
            mockForgotPasswordService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);
            


            var handler = new ForgotPasswordCommandHandler(mockForgotPasswordService.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.NotFound);
            Assert.Equal(ResponseType.NotFound, result.ResponseType);

            //Verify interactions
            mockForgotPasswordService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);

        }

        [Fact]

        public async Task HandleAsync_ReturnSuccess_WhenUserIsFound()
        {
            //Arrange
            var mockForgotPasswordService = new Mock<IUserRepository>();
            mockForgotPasswordService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

           
            //Act
            var handler = new ForgotPasswordCommandHandler(mockForgotPasswordService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ResponseType == ResponseType.Success);

            mockForgotPasswordService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        
    }
}
