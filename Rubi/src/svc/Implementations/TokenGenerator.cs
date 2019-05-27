using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rubi.Data.Models;
using Rubi.src.svc.contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rubi.src.svc.Implementations
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        public TokenGenerator(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;

        }

        public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var roles = await this.userManager.GetRolesAsync(user);

            var userRole = roles.ToList().FirstOrDefault();

            var claimIdentity = new ClaimsIdentity();

            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole),
            };

            claimIdentity.AddClaims(claimsList);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["JwtKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                //Audience = this.configuration["JwtIssuer"],
                //Issuer = this.configuration["JwtIssuer"],
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(this.configuration["JwtExpireDays"])),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenCreator = tokenHandler.CreateToken(tokenDescriptor);

            var userToken = tokenHandler.WriteToken(tokenCreator);

            return userToken;
        }
    }
}
