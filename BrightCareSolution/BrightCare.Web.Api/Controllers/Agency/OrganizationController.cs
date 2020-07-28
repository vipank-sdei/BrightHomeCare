using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Service.Interface.Agency.Organizations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService iOrganizationService;

        public OrganizationController(IOrganizationService iOrganizationService)
        {
            this.iOrganizationService = iOrganizationService;
        }

        [HttpGet]        
        public IActionResult GetOrganizations()
        {
            iOrganizationService.GetOrganizations();
            return Ok();
        }
    }
}