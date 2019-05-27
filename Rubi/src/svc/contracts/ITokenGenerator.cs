using Rubi.Data.Models;
using Rubi.src.svc.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.src.svc.contracts
{
    public interface ITokenGenerator
    {
        Task<TokenGenModel> GenerateJwtTokenAsync(ApplicationUser user);
    }
}
