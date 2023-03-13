using Application.Common.Interfaces;
using Domain.Interfaces;
using Infrastructure.Authentication.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class TokenManager : ITokenManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TokenManager(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GenerateToken(Guid id, string email)
        {
            var signingCredendials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
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
