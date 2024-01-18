using Moq;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.GetAllUsers;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApi.Test.AuthenticationTests.QueryHandlers
{
    public class GetAllUsersHandlerTest
    {
        

        [Fact]

        public async Task HandleAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockAuthManager = new Mock<IAuthenticationManager>();

            var usersList = new List<User>(); // Assuming User is the type returned by _UserRepo.GetAll()
            mockUserRepo.Setup(repo => repo.GetAll()).ReturnsAsync(usersList);

            var handler = new GetAllUsersQueryHandler(mockUserRepo.Object, mockAuthManager.Object);
            var request = new GetAllUsersQuery(); // Assuming GetAllUsersQuery is the query class

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.PayLoad);
            Assert.Same(usersList, result.PayLoad); // Ensure the payload is the same list returned by the mock



        }
    }
}
