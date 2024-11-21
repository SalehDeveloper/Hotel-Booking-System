using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(AppDbContext context , UserManager<ApplicationUser> userManager , IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _userManager = userManager; 
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> DeActivateUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user.IsActive)
            {
                user.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
             
            }
            return false;
        }
        public async Task<bool> ActivateUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (!user.IsActive)
            {
                user.IsActive = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userEmail =  _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            return await _userManager.FindByEmailAsync(userEmail);
        }
        public async Task<string> GetCurrentUserNameAsync()
        {
            var currentUser =await GetCurrentUserAsync();
            return currentUser.UserName;
        }
        public async Task<int> GetUserRoleLevelAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            Enum.TryParse<enRoles>(userRoles.First(), true, out enRoles res);

            return (int)res;
        }
        public async Task<bool> SetSuccessfullActivationSettings(ApplicationUser user , ApplicationUser byUser)
        {
            user.LastModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = byUser.UserName;

            var res =   await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                
                return true;
            
            }
                return false;


        }
        public async Task SetSuccessfullChangeRoleSettings (ApplicationUser user , ApplicationUser byUser)
        {

            user.LastModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = byUser.UserName;
            await _userManager.UpdateAsync(user);
        }
        public async Task ChangeUserRoleAsync(ApplicationUser user, string role)
        {
            var userRole = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, userRole.First());

            await _userManager.AddToRoleAsync(user, role);


        }
        public async Task<IEnumerable<ApplicationUser>> GetActiveUsers()
        {
           
            var activeUsers=await  _context.Users.Where(x => x.IsActive).Include(x=>x.Country).Include(x=>x.State).ToListAsync();    

            if(activeUsers.Any())   
                return activeUsers;
            return Enumerable.Empty<ApplicationUser>();
        }
        public async Task<IEnumerable<ApplicationUser>> GetInActiveUsers()
        {

            var activeUsers = await _context.Users.Where(x => ! x.IsActive).Include(x => x.Country).Include(x => x.State).ToListAsync();

            if (activeUsers.Any())
                return activeUsers;
            return Enumerable.Empty<ApplicationUser>();


        }
        public async Task<IEnumerable<ApplicationUser>> GetLastAddedUsers( int days)
        {
            var recentDate = DateTime.UtcNow.AddDays(-days); // Calculate the date for 'days' ago
            var users = await _context.Users.Include(x=>x.Country).Include(x=>x.State)
                .Where(x => x.CreatedAt >= recentDate) // Filter users created after this date
                .ToListAsync();

            if (users.Any()) return users;
            return Enumerable.Empty<ApplicationUser>();
        }


    }
}
