using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Workers.Server.Models;

namespace Workers.Server.Model.Services
{
    public class JWTTokenService
    {
        private IConfiguration _configuration;

        private SignInManager<ApplicationUser> _signInManager;

        public JWTTokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public static TokenValidationParameters GetValidationParamerts(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null)
            {
                throw new InvalidOperationException("JWT:Secret Key not Found");
            }

            var secretByets = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretByets);
        }

        public async Task<string> GetToken(ApplicationUser user, TimeSpan expireIn)
        {
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            if (principal == null)
            {
                throw new InvalidOperationException("The principla not found");
            }
        
            var signingKey = GetSecurityKey(_configuration);
            var toekn = new JwtSecurityToken(
                expires: DateTime.UtcNow + expireIn,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                claims: principal.Claims
                );
            return new JwtSecurityTokenHandler().WriteToken(toekn);
        }
    }
}
