using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.Services.EmailChecker.Contracts
{
    public interface IEmailChecker
    {
        bool EmailExist(string email);
    }
}
