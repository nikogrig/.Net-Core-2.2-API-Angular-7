using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rubi.Data.Models;
using Rubi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.Controllers
{
    [Route("api/profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // /api/profile/{id}
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var role = await this.userManager.GetRolesAsync(user);

            var profile = new ProfileDto
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                Role = role.FirstOrDefault()
            };

            if (profile != null)
            {
                return Ok(profile);
            }

            return Unauthorized();
        }
    }
}
