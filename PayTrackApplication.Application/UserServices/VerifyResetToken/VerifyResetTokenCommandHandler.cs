using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.VerifyResetToken
{
    internal class VerifyResetTokenCommandHandler: CommandHandlerBase<VerifyResetTokenCommand>
    {
        public VerifyResetTokenCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager) : base(userRepo, authManager)
        {
        }

        public override async Task<ActionResponse> Handle(VerifyResetTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user == null || user.ResetTokenExpires < DateTime.UtcNow) return new ActionResponse("Invalid Token", ResponseType.BadRequest);

            user.ResetPasswordLifeSpan = DateTime.UtcNow.AddMinutes(15);
            return await _UserRepo.UpdateEntity(user);
        }
    }
}
