using MediatR;
using Microsoft.AspNetCore.Http;
using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.LoginUser
{
    internal class LoginUserCommandHandler: IRequestHandler<LoginUserCommand, ActionResponse>
    {
        //private readonly HttpContext? _httpContext;
        internal readonly IUserRepository _UserRepo;
        internal readonly IAuthenticationManager _authManager;

        public LoginUserCommandHandler(IUserRepository userRepo,
            IAuthenticationManager authManager
            /*IHttpContextAccessor httpContextAccessor*/)
        {
            _UserRepo = userRepo;
            _authManager = authManager;
            //_httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<ActionResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user is null) return new ActionResponse("Email Or Password Is Invalid.");
            if (_authManager.VerifyPassword(user, request.Password) is false) return new ActionResponse("Email Or Password Is Invalid.");

            var token = _authManager.GenerateToken(user);
            return new ActionResponse { PayLoad = token };
        }
    }
}
