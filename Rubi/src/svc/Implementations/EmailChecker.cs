using Microsoft.AspNetCore.Identity;
using Rubi.Data.Models;
using Rubi.src.svc.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.src.svc.Implementations
{
    public class EmailChecker : IEmailChecker
    {
        private readonly UserManager<ApplicationUser> userManager;
        public EmailChecker(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public bool EmailExist(string email)
        {
            return this.userManager.FindByEmailAsync(email) == null;
        }
    }
}
