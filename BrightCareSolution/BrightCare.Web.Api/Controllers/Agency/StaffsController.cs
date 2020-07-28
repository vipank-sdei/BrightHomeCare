using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency;
using BrightCare.Service.Interface.Agency.Staffs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class StaffsController : BaseController
    {
        private readonly IStaffService _staffService;
        public StaffsController(IStaffService staffService)
        {
            this._staffService = staffService;
        }
        [HttpPost]
        public ActionResult CreateUpdateStaff(StaffsDTO staffsDTO)
        {
            return Ok(_staffService.CreateUpdateStaff(staffsDTO, GetToken(HttpContext)));
        }
        [HttpDelete]
        public ActionResult DeleteStaff(int id)
        {
            return Ok(_staffService.DeleteStaff(id, GetToken(HttpContext)));
        }
        [HttpGet]
        public ActionResult GetStaff(int? id)
        {
            return Ok(_staffService.GetStaff(id, GetToken(HttpContext)));
        }
    }
}
