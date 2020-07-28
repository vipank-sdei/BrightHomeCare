using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.UserRole;
using BrightCare.Service.Interface.Agency.UserRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class UserRoleController : BaseController
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService _userRoleService)
        {
            this._userRoleService = _userRoleService;
        }

        [HttpGet]
        public ActionResult GetUserRole()
        {
            return Ok(_userRoleService.GetUserRole(GetToken(HttpContext)));
        }

        [HttpPost]
        public ActionResult SaveUserRole(UserRoleDTO userRoleDTO)
        {
            return Ok(_userRoleService.AddUserRole(userRoleDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteUserRole(int RoleId)
        {
            return (_userRoleService.DeleteUserRole(RoleId, GetToken(HttpContext)));
        }
    }
}
