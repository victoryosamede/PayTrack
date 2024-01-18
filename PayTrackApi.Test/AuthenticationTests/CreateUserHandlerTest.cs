using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using PayTrackApplication.Application.UserServices.CreateUser;
using static PayTrackApplication.Domain.Constants.Enums;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.UsersFolder;
using System.Linq.Expressions;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application;

namespace PayTrackApi.Test.ControllerTest
{
    public class CreateUserHandlerTest
    {

        CreateUserCommand request = new CreateUserCommand
        {   Email = "victory@example.com",
            UserName = "victory",
            Password = "password",
            Role = Roles.User
        };
        [Fact]
        public async Task Handle_ReturnError_WhenUserAlreadyExists()
        {
            //Arrange
            var existingUser = new User
            { 
                Email = "victory@example.com",
                UserName = "victory",
                Roles = Roles.User
            };  
            var mockCreateUserService = new Mock<IUserRepository>();
            mockCreateUserService.Setup(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));

            var handler = new CreateUserCommandHandler (mockCreateUserService.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseType.BadRequest,result.ResponseType);

            //Verify interaction
            mockCreateUserService.Verify(service => service.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()) , Times.Once());
            authManagerMock.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
            mockCreateUserService.Verify(service => service.AddEntity(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ReturnActionResponse_UserCreatedSuccessfully()
        {
            //Arrange
            User? existingUser = null;
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(repo => repo.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);
            userRepoMock.Setup(repo => repo.AddEntity(It.IsAny<User>())).Returns((User user)=> Task.FromResult(new ActionResponse()));
            var authManagerMock = new Mock<IAuthenticationManager>();
            authManagerMock.Setup(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()));

            var handler = new CreateUserCommandHandler(userRepoMock.Object, authManagerMock.Object);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("", result.ReasonPhrase);
            Assert.NotNull(result.PayLoad);

            //Verify interactions
            userRepoMock.Verify(repo => repo.FindOneByExpression(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            userRepoMock.Verify(repo => repo.AddEntity(It.IsAny<User>()), Times.Once);
            authManagerMock.Verify(manager => manager.ManagePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);

        }
        
    }
}
 