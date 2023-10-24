using PayTrackApplication.Application.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ForgotPassword
{
    public class ForgotPasswordCommand: Request
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
