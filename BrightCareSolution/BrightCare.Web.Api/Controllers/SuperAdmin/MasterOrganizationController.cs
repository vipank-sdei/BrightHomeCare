using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Service.Interface.SuperAdmin.Organizations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.SuperAdmin
{
    [Route("api/webadmin/[controller]")]
    [ApiController]
    public class MasterOrganizationController : BaseController
    {
        private readonly IMasterOrganizationService iMasterOrganizationService;

        public MasterOrganizationController(IMasterOrganizationService iMasterOrganizationService)
        {
            this.iMasterOrganizationService = iMasterOrganizationService;
        }
        [HttpGet]
        [Route("GetOrganizations")]
        public IActionResult GetOrganizations()
        {
            iMasterOrganizationService.GetOrganizations();
            return Ok();
        }
    }
}