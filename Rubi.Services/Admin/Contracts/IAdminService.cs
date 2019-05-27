using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubi.Services.Admin.Models;

namespace Rubi.Services.Admin.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<UsersListingServiceModel>> GetAllUsersAsync();

        Task<UsersListingServiceModel> GetUserDetailByIdAsync(string id);

        Task<EditUserServiceModel> UpdateUserDataByIdAsync(string id, string username, string firstName, string lastName, string phoneNumber, string address, DateTime birthdate);
    }
}
