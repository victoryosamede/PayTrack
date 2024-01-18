using MediatR;
using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.VerifyResetToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ResetPassword
{
    internal class ResetPasswordCommandHandler: IRequestHandler<ResetPasswordCommand, ActionResponse>
    {
        internal readonly IUserRepository _UserRepo;
        internal readonly IAuthenticationManager _authManager;
        public ResetPasswordCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager)
        {
            _UserRepo = userRepo;
            _authManager = authManager;
        }
        public async Task<ActionResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user == null) return new ActionResponse("User Not Found", ResponseType.NotFound);
            if (user.ResetTokenExpires < DateTime.UtcNow) return new ActionResponse("Session has expired");
            if (user.PasswordResetToken != request.Token) return new ActionResponse("Invalid Token");

            _authManager.ManagePassword(user, request.NewPassword);
            return await _UserRepo.UpdateEntity(user);
        }
    }
}
