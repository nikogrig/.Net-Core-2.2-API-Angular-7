using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rubi.Data.Models;
using Rubi.Dtos;
using Rubi.Services.Auth.Contracts;
using Rubi.src.svc.contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Rubi.Constants.IdentitiesConstants;

namespace Rubi.Controllers
{
    //[EnableCors("CorsPolicy")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthService authService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ITokenGenerator tokenGenerator;

        public AccountController(ITokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManager, IAuthService authService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.tokenGenerator = tokenGenerator;
            this.roleManager = roleManager;
            this.authService = authService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        // /register
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterFormDto model)
        {
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

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, CUSTOMER_ROLE);

                await this.signInManager.SignInAsync(user, isPersistent: false);

                return Ok(Authorize(user));
            }

            return Unauthorized();
        }

        // /login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormDto model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                var appUser = this.userManager
                    .Users
                    .FirstOrDefault(r => r.Email == model.Email);

                return Ok(Authorize(appUser));
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

        private async Task<object> Authorize(ApplicationUser user)
        {
            var userToken = await this.tokenGenerator.GenerateJwtTokenAsync(user);

            if (userToken == null)
            {
                throw new ArgumentNullException("TODO JWT Generator token null exception description");
            }

            var model = new JwtAuthDto
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                Role = userToken.Role,
                Token = userToken.Token
            };

            if (model == null)
            {
                throw new ArgumentNullException("TODO JWT Generator model null exception description");

            }

            return model;
        }
    }
}