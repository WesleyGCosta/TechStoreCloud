using Api.Models.Response;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration) : IAuthService
    {

        public async Task<AccessResponse?>  AuthenticateAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user is null)
                return null;
            var passwordValid = await userManager.CheckPasswordAsync(user, password);
            if (!passwordValid) return null;



            return await GenerateJwtToken(user);

        }

        private async Task<AccessResponse> GenerateJwtToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            var stamp = await userManager.GetSecurityStampAsync(user);

            var refreshClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("stamp", stamp!)
            };

            var refreshToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: refreshClaims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new AccessResponse(
                Type: "Bearer",
                Token: new JwtSecurityTokenHandler().WriteToken(token),
                ExpireIn: token.ValidTo,
                RefreshToken: new JwtSecurityTokenHandler().WriteToken(refreshToken)
            );
        }
    }
}
