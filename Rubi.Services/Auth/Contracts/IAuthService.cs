using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.Models;
using WebApp.Services.Auth.Models;

namespace WebApp.Services.Auth.Contracts
{
    public interface IAuthService
    {
        Task<IEnumerable<UserServiceModel>> GetAllAsync();
    }
}
