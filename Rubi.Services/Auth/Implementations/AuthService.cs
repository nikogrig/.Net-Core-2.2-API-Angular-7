using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubi.Data;
using Rubi.Data.Models;
using Rubi.Services.Auth.Contracts;
using Rubi.Services.Auth.Models;

namespace Rubi.Services.Auth.Implementations
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
