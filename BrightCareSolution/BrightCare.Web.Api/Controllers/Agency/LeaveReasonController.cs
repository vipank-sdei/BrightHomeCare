using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.LeaveReasons;
using BrightCare.Service.Interface.Agency.LeaveReasons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class LeaveReasonController : BaseController
    {
        private readonly ILeaveReasonService _leaveReasonService;
        public LeaveReasonController(ILeaveReasonService _leaveReasonService)
        {
            this._leaveReasonService = _leaveReasonService;
        }

        [HttpGet]
        public ActionResult GetLeaveReason()
        {
            return Ok(_leaveReasonService.GetLeaveReason(GetToken(HttpContext)));
        }

        [HttpPost]
        public ActionResult SaveLeaveReason(LeaveReasonDTO leaveReasonDTO)
        {
            return Ok(_leaveReasonService.AddUpdateLeaveReason(leaveReasonDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeletLeaveType(int Id)
        {
            return (_leaveReasonService.DeletLeaveReason(Id, GetToken(HttpContext)));
        }
    }
}
