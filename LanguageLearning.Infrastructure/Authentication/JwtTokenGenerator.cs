using System.Text;
using LanguageLearning.Application.Common.Models;
using LanguageLearning.Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace LanguageLearning.Infrastructure.Authentication
{

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(string userId, string email)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Secret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _settings.ExpirationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public string GenerateToken(Guid userId, string email)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Secret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _settings.ExpirationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
