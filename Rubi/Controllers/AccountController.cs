using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rubi.Data.Models;
using Rubi.Dtos;
using Rubi.Services.Auth.Contracts;
using Rubi.src.svc.contracts;
using Rubi.Validators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Rubi.Constants.IdentitiesConstants;
using Microsoft.EntityFrameworkCore;

namespace Rubi.Controllers
{
    //[EnableCors("CorsPolicy")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        //private readonly ITokenGenerator tokenGenerator;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        //private readonly IEmailChecker emailCheckerService;

        public AccountController(//ITokenGenerator tokenGenerator,  
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            //this.tokenGenerator = tokenGenerator;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
           // this.emailCheckerService = emailCheckerService;
        }

        // /register
        // added fluent validation
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterFormDto model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Birthdate = model.Birthdate,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (user == null)
            {
                return StatusCode(403, "User cannot be null");
            }

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, CUSTOMER_ROLE);

                await this.signInManager.SignInAsync(user, isPersistent: false);

                return Ok(GenerateJwtToken(user));
            }

            return Unauthorized();
        }

        // /login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormDto model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                var appUser = await this.userManager
                    .Users
                    .FirstOrDefaultAsync(r => r.Email == model.Email);

                return Ok(GenerateJwtToken(appUser));
            }

            return Unauthorized();
        }

        // /logout                    
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()//[FromRoute]JwtAuthDto model)
        {
            await signInManager.SignOutAsync();

            return Ok("Successfully loged out! Come back again!");
        }

        private object GenerateJwtToken(ApplicationUser user)
        {
            var roles = this.userManager.GetRolesAsync(user);

            var userRole = string.Empty;

            foreach (var role in roles.Result)
            {
                userRole = role;
                break;
            }

            var claimIdentity = new ClaimsIdentity();

            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole)
            };

            claimIdentity.AddClaims(claimsList.ToList());

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["JwtKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                //Audience = this.configuration["JwtIssuer"],
                //Issuer = this.configuration["JwtIssuer"],
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(configuration["JwtExpireDays"])),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenCreator = tokenHandler.CreateToken(tokenDescriptor);

            var userToken = tokenHandler.WriteToken(tokenCreator);

            var model = new JwtAuthDto
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                Role = userRole,
                Token = userToken
            };

            return model;
        }
    }
}