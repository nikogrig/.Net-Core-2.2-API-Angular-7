using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rubi.Common;
using Rubi.Data.Models;

namespace Rubi.Services.Admin.Models
{
    public class UsersListingServiceModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }


    }
}
