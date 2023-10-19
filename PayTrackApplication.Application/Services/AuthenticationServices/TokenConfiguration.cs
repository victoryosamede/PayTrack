using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.Services.AuthenticationServices
{
    public class TokenConfiguration
    {
        public TimeSpan AccessTokenLifeSpan { get; set; } = TimeSpan.FromMinutes(30);
        public TimeSpan RefreshTokenLifeSpan { get; set; } = TimeSpan.FromHours(5);
        public required string SecretKey { get; set; }
    }
}
