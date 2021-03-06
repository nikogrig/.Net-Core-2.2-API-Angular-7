﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubi.Common;
using Rubi.Data.Models;

namespace Rubi.Services.Auth.Models
{
    public class UserServiceModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
