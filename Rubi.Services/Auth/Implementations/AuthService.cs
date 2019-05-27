using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Services.Auth.Contracts;
using WebApp.Services.Auth.Models;

namespace WebApp.Services.Auth.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext db;

        public AuthService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserServiceModel>> GetAllAsync()
        {
            var users = await this.db
                .Users
                .ProjectTo<UserServiceModel>()
                .ToListAsync();

                return users;
        }
    }
}
