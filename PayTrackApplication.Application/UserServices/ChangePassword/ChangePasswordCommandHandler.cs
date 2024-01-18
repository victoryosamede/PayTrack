using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ChangePassword
{
    internal class ChangePasswordCommandHandler: CommandHandlerBase<ChangePasswordCommand>
    {
        public ChangePasswordCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager) : base(userRepo, authManager)
        {
        }
        public override async Task<ActionResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.GetById(request.Id);
            if (user == null) return new ActionResponse("User is not found.", ResponseType.NotFound);

            if (!_authManager.VerifyPassword(user, request.CurrentPassword))
                return new ActionResponse("Current password is invalid", ResponseType.BadRequest);

            _authManager.ManagePassword(user, request.NewPassword);
            return await _UserRepo.UpdateEntity(user);
        }
    }
}
