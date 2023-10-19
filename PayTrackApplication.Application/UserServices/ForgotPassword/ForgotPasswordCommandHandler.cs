using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ForgotPassword
{
    internal class ForgotPasswordCommandHandler: CommandHandlerBase<ForgotPasswordCommand>
    {
        public ForgotPasswordCommand _createRandomToken = new ForgotPasswordCommand();
        public ForgotPasswordCommandHandler(IUserRepository userRepo, IAuthenticationManager authManager, ForgotPasswordCommand createRandomToken) : base(userRepo, authManager)
        {
            _createRandomToken = createRandomToken;
        }
        public override async Task<ActionResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.FindOneByExpression(x => x.Email == request.Email);
            if (user == null) return new ActionResponse("User Not Found", ResponseType.NotFound);

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddMinutes(30);

            return new ActionResponse("You May Now Reset Your Password", ResponseType.Success);
        }
        internal string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
