using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Models;
using HootelBooking.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IOptions<JwtHelper> _jwt;
        private readonly IOptions<EmailConfiguration> _emailConfiguration;

        public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IOptions<JwtHelper> jwt, IOptions<EmailConfiguration> emailConfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwt = jwt;
            _emailConfiguration = emailConfiguration;
        }

        public async Task<ApplicationUser> RegisterAsync(ApplicationUser user, string password)
        {

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, enRoles.USER.ToString()); 
                
                return user;
            }
            return null;


        }
        public async Task<string> LoginAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var jwtSecurityToken = await CreateJWTToken(user);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return token;

        }
        private async Task<JwtSecurityToken> CreateJWTToken(ApplicationUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {

                new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim(ClaimTypes.Name, user.UserName)



            }.Union(userClaims)
             .Union(roleClaims);

            var symmatricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.key));
            var signingCredentials = new SigningCredentials(symmatricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                  issuer: _jwt.Value.Issuer,
                  audience: _jwt.Value.Audience,
                  claims: claims,
                  expires: DateTime.Now.AddDays(_jwt.Value.DuartionInDays),
                  signingCredentials: signingCredentials



                );


            return jwtSecurityToken;


        }

        public async Task<bool> ResetPasswordAsync(ApplicationUser user, string newPassword)
        {

            var res = await _userManager.RemovePasswordAsync(user);
            if (res.Succeeded)
            {
                var finalRes = await _userManager.AddPasswordAsync(user, newPassword);

                if (finalRes.Succeeded)
                    return true;
                return false;
            }
            return false;
        }

     

        public async Task<bool> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var isPasswordChanged = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (isPasswordChanged.Succeeded)
                return true;
            return false;
        }

        public async Task ClearExpiredCodes()
        {

            var usersWithExpiredCodes = new List<ApplicationUser>();

            foreach (var user in _userManager.Users.Where(u =>
                        u.TwoFactorCodeExpiry < DateTime.UtcNow ||
                        u.ConfirmationCodeExpiry < DateTime.UtcNow ||
                        u.ResetPasswordCodeExpiry < DateTime.UtcNow))
            {
                bool hasExpiredCode = false;

                if (user.TwoFactorCodeExpiry < DateTime.UtcNow)
                {
                    user.TwoFactorCodeExpiry = null;
                    user.TwoFactorCode = null;
                    hasExpiredCode = true;
                }

                if (user.ConfirmationCodeExpiry < DateTime.UtcNow)
                {
                    user.ConfirmationCodeExpiry = null;
                    user.EmailConfirmationCode = null;
                    hasExpiredCode = true;
                }

                if (user.ResetPasswordCodeExpiry < DateTime.UtcNow)
                {
                    user.ResetPasswordCodeExpiry = null;
                    user.ResetPasswordCode = null;
                    hasExpiredCode = true;
                }

                if (hasExpiredCode)
                {
                    usersWithExpiredCodes.Add(user);
                }
            }

            await Task.WhenAll(usersWithExpiredCodes.Select(user => _userManager.UpdateAsync(user)));

        }

        public async Task SetRegisterCodeSettings(ApplicationUser user, string code)
        {
              user.EmailConfirmationCode = code;    
              user.ConfirmationCodeExpiry = DateTime.UtcNow.AddMinutes(5);
            await _userManager.UpdateAsync(user);


        }
     
        public async Task SetLogin2FactorCodeSettings(ApplicationUser user, string code)
        {
            user.TwoFactorCode = code;  
            user.TwoFactorCodeExpiry = DateTime.UtcNow.AddMinutes(2);
            await _userManager.UpdateAsync(user);   
        }

        public async Task SetSuccessfullRegisterationSettings (ApplicationUser user)
        {
            user.EmailConfirmationCode = null;
            user.ConfirmationCodeExpiry = null;
            user.EmailConfirmed = true;
            user.IsActive = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task SetSuccessfullLoginSettings (ApplicationUser user)
        {

            user.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
        }

        public async Task SetSuccessfullLoginWith2FactorSettings(ApplicationUser user)
        {
            user.LastLogin = DateTime.UtcNow;
            user.TwoFactorCodeExpiry = null;
            user.TwoFactorCode = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task SetSuccessfullResetPasswordSettings(ApplicationUser user)
        {
            user.ResetPasswordCode = null;
            user.ResetPasswordCodeExpiry = null;
            user.ModifiedBy = "system";
            user.LastModifiedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
        }

        public async Task SetSuccessfullChangePasswordSettings(ApplicationUser user)
        {
            user.ModifiedBy = "system";
            user.LastModifiedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

        }

        public async Task<bool> SetForgotPasswordSettingsAsync(ApplicationUser user, string code)
        {

            user.ResetPasswordCode = code;
            user.ResetPasswordCodeExpiry = DateTime.UtcNow.AddMinutes(2);

            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
                return true;
            return false;

        }

        public bool DoesEmailConfirmationCodeValid(ApplicationUser user, string code)
        {
            if (user.EmailConfirmationCode != code || user.ConfirmationCodeExpiry < DateTime.UtcNow)
                return false;
            return true; 
        }

        public bool DoesResetPasswordCodeValid(ApplicationUser user, string code)
        {
            if (user.ResetPasswordCode != code || user.ResetPasswordCodeExpiry < DateTime.UtcNow)
                return false;
            return true;
        }

        public bool Does2FactorCodeValid(ApplicationUser user, string code)
        {
            if (user.TwoFactorCode != code || user.TwoFactorCodeExpiry < DateTime.UtcNow)
                return false;
            return true;
        }

        public async Task<bool> IsPasswordCorrect(ApplicationUser user, string password)
        {
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordCorrect)
            {
                await _userManager.AccessFailedAsync(user);

                return false;
            }

            await _userManager.ResetAccessFailedCountAsync(user);
            return true;
        }

        public async Task<bool> IsAccountLockedOut(ApplicationUser user)
        {

            if (await _userManager.IsLockedOutAsync(user))
            {
                return true;

            }
            return false;
        }
    }
}