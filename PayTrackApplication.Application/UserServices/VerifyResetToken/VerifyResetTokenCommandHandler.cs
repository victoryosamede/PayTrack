using MediatR;
using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.VerifyResetToken
{
    internal class VerifyResetTokenCommandHandler: IRequestHandler<VerifyResetTokenCommand, ActionResponse>
    {
        internal readonly IUserRepository _UserRepo;
        
        public VerifyResetTokenCommandHandler(IUserRepository userRepo)
        {
            _UserRepo = userRepo;
            
        }

        public async Task<ActionResponse> Handle(VerifyResetTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user == null || user.ResetTokenExpires < DateTime.UtcNow) return new ActionResponse("Invalid Token", ResponseType.NotFound);

            user.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);
            return await _UserRepo.UpdateEntity(user);
        }
    }
}
