using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.Services.AuthenticationServices
{
    public interface IAuthenticationManager
    {
        void ManagePassword(User user, string password);
        TokenModel GenerateToken(User user);
        bool VerifyPassword(User user, string password);
    }
}
