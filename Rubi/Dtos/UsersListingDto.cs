using Rubi.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubi.Dtos
{
    public class UsersListingDto
    {
        public IEnumerable<UsersListingServiceModel> Users { get; set; }
    }
}
