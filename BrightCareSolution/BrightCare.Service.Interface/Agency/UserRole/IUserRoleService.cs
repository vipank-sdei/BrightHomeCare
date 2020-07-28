using BrightCare.Common;
using BrightCare.Common.IService;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.UserRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.UserRole
{
    public interface IUserRoleService :IBaseService
    {
        JsonModel GetUserRole(TokenModel token);
        JsonModel AddUserRole(UserRoleDTO userRoleDTO, TokenModel token);
        bool DeleteUserRole(int RoleId, TokenModel token);
    }
}
