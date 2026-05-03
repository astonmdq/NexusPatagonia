using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexusPatagonia.Infrastructure.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        public AuthService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
            {
                return null;
            }

            // Usar el hash almacenado del usuario; no volver a hashear la contraseña proporcionada
            if (string.IsNullOrEmpty(user.PasswordHash) || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId",user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            // Si necesita incluir roles, obténgalos del repositorio o de donde estén almacenados
            // y añádalos aquí. Evitar el acceso a `user.Role` porque la clase User no lo define.

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            // Implement a secure hashing algorithm, e.g., BCrypt
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password,HashType.SHA256);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hashed version
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword,HashType.SHA256);
        }
    }
}
