using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.src.svc.contracts
{
    public interface IEmailChecker
    {
        bool EmailExist(string email);
    }
}
