using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.StaffLeave;
using BrightCare.Service.Interface.Agency.StaffLeave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class StaffLeaveController : BaseController
    {

        public readonly IStaffLeaveService _staffLeaveService;

        public StaffLeaveController(IStaffLeaveService iStaffLeaveService)
        {
            this._staffLeaveService = iStaffLeaveService;
        }

        [HttpPost]
        public ActionResult CreateUpdateStaffLeave(StaffLeaveDTO staffLeaveDTO)
        {
            return Ok(_staffLeaveService.CreateUpdateStaffLeave(staffLeaveDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public ActionResult DeleteAppliedLeave(int id)
        {
            return Ok(_staffLeaveService.DeleteAppliedLeave(id, GetToken(HttpContext)));
        }

        [HttpGet]
        public ActionResult GetStaffLeave(int? id)
        {
            return Ok(_staffLeaveService.GetStaffLeave(id,GetToken(HttpContext)));
        }
    }
}
