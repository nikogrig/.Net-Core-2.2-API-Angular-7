using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rubi.Data.Models;
using Rubi.Dtos;
using Rubi.Services.Auth.Contracts;
using Rubi.src.svc.contracts;
using Rubi.Validators;
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
        private readonly IEmailChecker emailCheckerService;

        public AccountController(IEmailChecker emailCheckerService, ITokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManager, IAuthService authService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.tokenGenerator = tokenGenerator;
            this.roleManager = roleManager;
            this.authService = authService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.emailCheckerService = emailCheckerService;
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

            var validator = new RegisterFormValidator(this.emailCheckerService);

            var validatorResult = validator.Validate(model);

            if (!validatorResult.IsValid)
            {
                foreach (var failure in validatorResult.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
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

                return Ok(Authorize(user));
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

            var validator = new LoginFormValidator();

            var validatorResult = validator.Validate(model);

            if (!validatorResult.IsValid)
            {
                foreach (var failure in validatorResult.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

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
                return StatusCode(500, "User cannot be null.");
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

            var validator = new JwtAuthValidator();

            var validatorResult = validator.Validate(model);

            if (!validatorResult.IsValid)
            {
                foreach (var failure in validatorResult.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            if (model == null)
            {
                return StatusCode(500, "Model cannot be null.");

            }

            return model;
        }
    }
}