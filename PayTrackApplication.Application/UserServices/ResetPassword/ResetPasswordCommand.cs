using PayTrackApplication.Application.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ResetPassword
{
    public class ResetPasswordCommand: Request
    {
        [Required]
        public required string Token { get; set; }

        public required string Email { get; set; }
        public required string NewPassword { get; set; }
    }
}
