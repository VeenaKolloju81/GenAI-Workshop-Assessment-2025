using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GenAI_Workshop_Assessment_2025.Services
{
    public class AuthService : IAuthService
    {
        // In-memory user store for demo purposes
        private readonly Dictionary<string, (string PasswordHash, string Role)> _users = new()
        {
            // Passwords are hashed using SHA256 for demo; use a stronger hash in production
            { "admin", (ComputeHash("adminpass"), "Admin") },
            { "user", (ComputeHash("userpass"), "User") }
        };

        // In-memory token blacklist for logout
        private readonly ConcurrentDictionary<string, DateTime> _invalidatedTokens = new();

        private readonly string _jwtSecret = "YourSuperSecretKeyForJwtTokenGeneration!"; // Should be in config
        private readonly int _jwtLifespanMinutes = 60;

        public string Login(string username, string password)
        {
            if (!_users.TryGetValue(username, out var userInfo))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var passwordHash = ComputeHash(password);
            if (userInfo.PasswordHash != passwordHash)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, userInfo.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtLifespanMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Logout(string token)
        {
            // Add token to blacklist with its expiry
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;
            _invalidatedTokens[token] = expiry;
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (_invalidatedTokens.ContainsKey(token))
                throw new SecurityTokenException("Token has been invalidated.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new SecurityTokenExpiredException("Token has expired.");
            }
            catch (Exception)
            {
                throw new SecurityTokenException("Invalid token.");
            }
        }

        private static string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}