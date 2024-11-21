using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using System;

namespace HootelBooking.Application.Contracts
{
    public interface IAuthRepository
    {
        //operations 
        Task<ApplicationUser> RegisterAsync(ApplicationUser user ,string password );
        Task <string> LoginAsync(string email);
        Task<bool> ResetPasswordAsync (ApplicationUser user , string newPassword);
        Task<bool> SetForgotPasswordSettingsAsync(ApplicationUser user , string code );
        Task<bool> ChangePasswordAsync(ApplicationUser user, string currentPassword , string newPassword);


        // setting for operation
        Task SetRegisterCodeSettings(ApplicationUser user, string code);
        Task SetLogin2FactorCodeSettings(ApplicationUser user, string code);

       


        //successfull registeration 
        Task SetSuccessfullRegisterationSettings(ApplicationUser user);
        Task SetSuccessfullLoginSettings(ApplicationUser user);
        Task SetSuccessfullLoginWith2FactorSettings(ApplicationUser user);
        Task SetSuccessfullResetPasswordSettings (ApplicationUser user);
        Task SetSuccessfullChangePasswordSettings(ApplicationUser user);
         

        //checks 
        bool DoesEmailConfirmationCodeValid (ApplicationUser user , string code);
        bool DoesResetPasswordCodeValid(ApplicationUser user, string code);
        bool Does2FactorCodeValid(ApplicationUser user, string code);
        Task<bool> IsPasswordCorrect(ApplicationUser user, string password);
        Task<bool> IsAccountLockedOut(ApplicationUser user);



        Task ClearExpiredCodes();
    }
}
