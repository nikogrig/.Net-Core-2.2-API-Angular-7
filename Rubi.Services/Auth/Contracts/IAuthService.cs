using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubi.Data.Models;
using Rubi.Services.Auth.Models;

namespace Rubi.Services.Auth.Contracts
{
    public interface IAuthService
    {
        Task<IEnumerable<UserServiceModel>> GetAllAsync();
    }
}
