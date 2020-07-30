using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.Staff;
using BrightCare.Service.Interface.Agency.Staffs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class StaffExperienceController : BaseController
    {
        public readonly IStaffExperienceService _staffExperienceService;
        public StaffExperienceController(IStaffExperienceService staffExperienceService)
        {
            this._staffExperienceService = staffExperienceService;
        }

        [HttpPost]
        public ActionResult CreateUpdateStaffExperience(StaffExperienceRequestDTO staffExperienceRequestDTO)
        {
            return Ok(_staffExperienceService.CreateUpdateStaffExperience(staffExperienceRequestDTO, GetToken(HttpContext)));
        }

        [HttpGet]
        public ActionResult GetStaffExperienceID(int id)
        {
            return Ok(_staffExperienceService.GetStaffExperienceByID(id, GetToken(HttpContext)));
        }

        [HttpGet("GetStaffExperienceByUserID")]
        public ActionResult GetStaffExperienceByUserID(int userid)
        {
            return Ok(_staffExperienceService.GetStaffExperienceByUserID(userid, GetToken(HttpContext)));
        }

        [HttpDelete]
        public ActionResult DeleteStaffExperience(int id)
        {
            return Ok(_staffExperienceService.DeleteStaffExperience(id, GetToken(HttpContext)));
        }
    }
}
