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
    internal class LoginUserCommandHandler: CommandHandlerBase<LoginUserCommand>
    {
        private readonly HttpContext? _httpContext;

        public LoginUserCommandHandler(IUserRepository userRepo,
            IAuthenticationManager authManager,
            IHttpContextAccessor httpContextAccessor) : base(userRepo, authManager)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public override async Task<ActionResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user is null) return new ActionResponse("Email Or Password Is Invalid.");
            if (user.PasswordHash != request.Password) return new ActionResponse("Email Or Password Is Invalid.");

            var token = _authManager.GenerateToken(user);
            return new ActionResponse { PayLoad = token };
        }
    }
}
