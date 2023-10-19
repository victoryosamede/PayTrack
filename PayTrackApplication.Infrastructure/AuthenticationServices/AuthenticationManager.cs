using static BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PayTrackApplication.Application.Services.AuthenticationServices;

namespace PayTrackApplication.Infrastructure.AuthenticationServices
{
    internal class AuthenticationManager: IAuthenticationManager
    {
        private readonly TokenConfiguration _configuration;

        public AuthenticationManager(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(_configuration.AccessTokenLifeSpan),
                signingCredentials: credentials
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void ManagePassword(User user, string password)
        {
            user.PasswordHash = HashPassword(password);
        }
        public bool VerifyPassword(User user, string password) =>
            Verify(password, user.PasswordHash);
    }
}
