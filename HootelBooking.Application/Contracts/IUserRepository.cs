using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IUserRepository:IBaseRepository<ApplicationUser>
    {
        Task<bool> DeActivateUserAsync(int id);
        Task<bool> ActivateUserAsync(int id);
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<string> GetCurrentUserNameAsync();
        Task<int> GetUserRoleLevelAsync(ApplicationUser user);
        Task<bool> SetSuccessfullActivationSettings(ApplicationUser user, ApplicationUser byUser);
        Task SetSuccessfullChangeRoleSettings(ApplicationUser user, ApplicationUser byUser);
        Task ChangeUserRoleAsync(ApplicationUser user, string role);
        Task<IEnumerable<ApplicationUser>> GetActiveUsers();
        Task<IEnumerable<ApplicationUser>> GetInActiveUsers();
        Task<IEnumerable<ApplicationUser>> GetLastAddedUsers(int days);
    }
}
