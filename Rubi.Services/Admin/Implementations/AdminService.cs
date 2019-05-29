using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubi.Data;
using Rubi.Services.Admin.Contracts;
using Rubi.Services.Admin.Models;

namespace Rubi.Services.Admin.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext db;

        public AdminService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UsersListingServiceModel>> GetAllUsersAsync()
        {
            var users = await this.db
                .Users
                .ProjectTo<UsersListingServiceModel>()
                .ToListAsync();

            return users;
        }

        public async Task<UsersListingServiceModel> GetUserDetailByIdAsync(Guid id)
        {
            var user = await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UsersListingServiceModel>()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<EditUserServiceModel> UpdateUserDataByIdAsync(Guid id, string username, string firstName, string lastName, string phoneNumber, string address, DateTime birthdate)
        {
            var user = await this.db
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            user.UserName = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Address = address;
            user.PhoneNumber = phoneNumber;
            user.Birthdate = birthdate;

            this.db.UpdateRange(user);

            await this.db.SaveChangesAsync();

            var updatedUser = await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<EditUserServiceModel>()
                .FirstOrDefaultAsync();

            return updatedUser;
        }
    }
}
