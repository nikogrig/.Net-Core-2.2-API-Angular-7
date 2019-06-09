using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubi.Data.Models;
using Rubi.Dtos;
using Rubi.Services.Admin.Contracts;
using Rubi.Validators;
using System;
using System.Threading.Tasks;
using static Rubi.Constants.IdentitiesConstants;

namespace Rubi.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = ADMINISTRATOR_ROLE)]
    public class AdminController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminService adminService;

        public AdminController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IAdminService adminService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.adminService = adminService;
        }

        // /get-roles
        [Route("get-roles")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await this.roleManager.Roles.ToListAsync();

            if (roles != null)
            {
                return Ok(roles);
            }

            return Unauthorized();
        }

        // /create-user
        // added fluentValidator
        [Route("create-user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserByAdminDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, model.Role);

                return Ok();
            }

            return Unauthorized();
        }

        // /delete-user/{id}
        [Route("delete-user/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return StatusCode(500, "ID cannot be null or empty");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return StatusCode(403, "User cannot be null");
            }

            var success = await this.userManager.DeleteAsync(user);

            if (success.Succeeded)
            {
                return Ok();
            }

            return StatusCode(500, $"Cannot delete User. Something went wrong");
        }

        // /edit-user/{id}
        [Route("edit-user/{id}")]
        [HttpPatch]
        public async Task<IActionResult> EditUser(Guid id, [FromBody]EditUserDto user)
        {
            if (user == null) //|| string.IsNullOrEmpty(id))
            {
                return Unauthorized();
            }
            var editedUser = await this.adminService
                .UpdateUserDataByIdAsync(id, user.Username, user.FirstName, user.LastName, user.PhoneNumber, user.Address, user.Birthdate);

            if (editedUser == null)
            {
                return Unauthorized();
            }

            return Ok(editedUser);
        }

        // /user-detail/{id}
        [Route("user-detail/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            var model = await this.adminService.GetUserDetailByIdAsync(id);

            if (model == null)
            {
                return Unauthorized();
            }

            return Ok(model);
        }

        // /get-users
        [Route("get-users")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var model = new UsersListingDto
            {
                Users = await this.adminService.GetAllUsersAsync()
            };

            if (model != null)
            {
                return Ok(model.Users);
            }

            return Unauthorized();
        }
    }

}
