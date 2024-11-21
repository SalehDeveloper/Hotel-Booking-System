using HootelBooking.Application.Contracts;
using HootelBooking.Application.Exceptions;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    { 
       
        public bool DoesAuthorizeRoleManagementForAddingAccept(int currentUserRoleLevel, int targetUserRoleLeve)
        {

            //Log.Information("Authorizing role management. Current user role level: {currentUserRoleLevel}, Target user role level: {targetUserRoleLeve}",
             //                 currentUserRoleLevel, targetUserRoleLeve);

            if (currentUserRoleLevel < targetUserRoleLeve )
            {

                return false; 
            }

            return true;
          //  Log.Information("Authorization successful.");

        }
        public bool DoesAuthorizeRoleManagementForChangingRoleAccept(int currentUserRoleLevel, int targetUserRoleLeve)
        {

            //Log.Information("Authorizing role management. Current user role level: {currentUserRoleLevel}, Target user role level: {targetUserRoleLeve}",
            //                 currentUserRoleLevel, targetUserRoleLeve);

            if (currentUserRoleLevel <= targetUserRoleLeve)
            {

                return false;
            }

            return true;
            //  Log.Information("Authorization successful.");

        }

        public bool DoesAuthorizeRoleManagementForDeletingAccept(int currentUserRoleLevel, int targetUserRoleLeve)
        {
            if (currentUserRoleLevel <= targetUserRoleLeve)
                return false;
            return true; 
        }
    }
}
