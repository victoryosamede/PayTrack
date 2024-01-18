using MediatR;
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
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ActionResponse>
    {
        internal readonly IUserRepository _UserRepo;
        internal readonly IAuthenticationManager _authManager;
        public CreateUserCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager)
        {
            _UserRepo = userRepo;
            _authManager = authManager;
        }

        public async Task<ActionResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user is not null) return new ActionResponse("User Already Exists.");

            User newUser = request;
            _authManager.ManagePassword(newUser, request.Password);

            var result= await _UserRepo.AddEntity(newUser);
            if (result.ResponseType != ResponseType.Success)
                return result;
            return new ActionResponse
            {
                PayLoad = new
                {
                    newUser.Id,
                    newUser.Email,
                    newUser.UserName,
                    Role = newUser.Roles.ToString()
                }
            };
        }
    }
}
