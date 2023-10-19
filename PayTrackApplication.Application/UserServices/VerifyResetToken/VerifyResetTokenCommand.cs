using PayTrackApplication.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.VerifyResetToken
{
    public class VerifyResetTokenCommand:CommandBase
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
