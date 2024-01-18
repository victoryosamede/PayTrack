using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApplication.Domain.Models.UsersFolder
{
    public class User: BaseModel
    {
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public string? PasswordHash { get; set; }
        public required Roles Roles { get; set; }
        public string? RefreshToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? RefreshTokenLifeSpan { get; set; }
        public bool IsFalseDelete { get; set; }
    }
}























