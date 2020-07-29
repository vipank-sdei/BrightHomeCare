using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.LeaveTypes;
using BrightCare.Service.Interface.Agency.LeaveTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class LeaveTypeController : BaseController
    {
        private readonly ILeaveTypeService _leaveTypeService;
        public LeaveTypeController(ILeaveTypeService _leaveTypeService)
        {
            this._leaveTypeService = _leaveTypeService;
        }


        [HttpGet]
        public ActionResult GetLeaveType()
        {
            return Ok(_leaveTypeService.GetLeaveType(GetToken(HttpContext)));
        }

        [HttpPost]
        public ActionResult SaveLeaveType(LeaveTypeDTO leaveTypeDTO)
        {
            return Ok(_leaveTypeService.AddUpdateLeaveType(leaveTypeDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeletLeaveType(int Id)
        {
            return (_leaveTypeService.DeletLeaveType(Id, GetToken(HttpContext)));
        }
    }
}
