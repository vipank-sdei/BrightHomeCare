using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Common.Service;
using BrightCare.Dtos.Agency.UserRole;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.UserRole;
using BrightCare.Service.Interface.Agency.UserRole;
using HC.Common.HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.UserRole
{
    public class UserRoleService: BaseService, IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository _userRoleRepository, IMapper mapper)
        {
            this._userRoleRepository = _userRoleRepository;
            _mapper = mapper;

        }

        public JsonModel GetUserRole(TokenModel token)
        {
            List<UserRoleDTO> userRoleDTO = new List<UserRoleDTO>();
            List<UserRoles> userRoles = _userRoleRepository.GetAll(l => l.IsDeleted == false && l.OrganizationID == 2).ToList();// token.OrganizationID);
            userRoleDTO = _mapper.Map<List<UserRoleDTO>>(userRoles); // Mapping

            return new JsonModel(userRoleDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }


        public JsonModel AddUserRole(UserRoleDTO userRoleDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            UserRoles userRolesEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;


            userRolesEntity = _mapper.Map<UserRoles>(userRoleDTO);
            userRolesEntity.OrganizationID = 2; // token.OrganizationID;           
            userRolesEntity.IsActive = true;
            _userRoleRepository.Create(userRolesEntity);
            _userRoleRepository.SaveChanges();
           
            return Result;
        }


        public bool DeleteUserRole(int RoleId, TokenModel token)
        {
            UserRoles userRolesEntity = _userRoleRepository.Get(l => l.Id == RoleId && l.OrganizationID == 2);// token.OrganizationID);
            userRolesEntity.IsDeleted = true;
            userRolesEntity.IsActive = false;
            userRolesEntity.DeletedBy = 2;// token.UserID;
            userRolesEntity.DeletedDate = DateTime.UtcNow;
            _userRoleRepository.SaveChanges();

            return true;
        }
    }
}
