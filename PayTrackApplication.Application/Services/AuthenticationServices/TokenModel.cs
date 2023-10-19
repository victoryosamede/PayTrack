using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.Services.AuthenticationServices
{
    public class TokenModel
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
