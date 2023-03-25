using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Infrastructure.Authentication.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class TokenManager : ITokenManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TokenManager(IDateTimeProvider dateTimeProvider, JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GenerateToken(Guid id, string email, Roles role)
        {
            var signingCredendials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: signingCredendials,
                expires: _dateTimeProvider.UtcNow.AddDays(_jwtSettings.DurationInDays));

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
