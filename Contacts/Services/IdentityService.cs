using Contacts.Jwt;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contacts.Services
{
    public interface IIdentityService
    {
        public Task<string> Login(LoginUser login);
        public Task SeedUser();
    }
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfiguration _config;
        public IdentityService(UserManager<IdentityUser> userManager, IOptions<JwtConfiguration> config)
        {
            _userManager = userManager;
            _config = config.Value;
        }
        public async Task<string> Login(LoginUser login)
        {
            var user = await _userManager.FindByNameAsync(login.Login);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    return await GenerateTokenString(user, _config);
                }
                throw new BadHttpRequestException("Wrong password.");
            }
            throw new BadHttpRequestException("Account with given login doesn't exist.");
        }
        public async Task<IdentityResult> Register(LoginUser registration)
        {
            var user = new IdentityUser
            {
                UserName = registration.Login
            };

            var result = await _userManager.CreateAsync(user, registration.Password);
            return result;
        }
        public async Task SeedUser()
        {
            var user = new LoginUser()
            {
                Login = "user",
                Password = "zaq1@WSX"
            };
            await Register(user);
        }
        private async Task<string> GenerateTokenString(IdentityUser user, JwtConfiguration jwtConfig)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.UserName) };

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
