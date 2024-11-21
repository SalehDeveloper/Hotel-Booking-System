using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface  IAuthorizationRepository
    {
        bool DoesAuthorizeRoleManagementForAddingAccept(int currentUserRoleLevel, int targetUserRoleLeve);

        bool DoesAuthorizeRoleManagementForDeletingAccept(int currentUserRoleLevel, int targetUserRoleLeve);

        bool DoesAuthorizeRoleManagementForChangingRoleAccept(int currentUserRoleLevel, int targetUserRoleLeve);

    }
}
