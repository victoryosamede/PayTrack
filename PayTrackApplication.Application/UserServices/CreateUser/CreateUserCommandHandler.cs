using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.CreateUser
{
    internal class CreateUserCommandHandler : CommandHandlerBase<CreateUserCommand>
    {
        public CreateUserCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager) : base(userRepo, authManager)
        {
        }

        public override async Task<ActionResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user is not null) return new ActionResponse("User Already Exist.");

            User newUser = request;
            _authManager.ManagePassword(newUser, request.Password);

            return await _UserRepo.AddEntity(newUser);
        }
    }
}
