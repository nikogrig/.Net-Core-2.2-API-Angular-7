using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.src.svc.contracts
{
    interface ITokenGenerator
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    }
}
