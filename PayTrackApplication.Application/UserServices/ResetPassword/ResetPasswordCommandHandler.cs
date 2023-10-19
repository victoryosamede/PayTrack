using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ResetPassword
{
    internal class ResetPasswordCommandHandler: CommandHandlerBase<ResetPasswordCommand>
    {
        public ResetPasswordCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager) : base(userRepo, authManager)
        {
        }
        public override async Task<ActionResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user == null) return new ActionResponse("User Not Found", ResponseType.NotFound);
            if (user.ResetPasswordLifeSpan < DateTime.UtcNow) return new ActionResponse("Session has expired");
            if (user.PasswordResetToken != request.Token || user.ResetTokenExpires < DateTime.Now) return new ActionResponse("Invalid Token");

            _authManager.ManagePassword(user, request.NewPassword);
            return await _UserRepo.UpdateEntity(user);
        }
    }
}
