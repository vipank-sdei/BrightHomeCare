using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.MasterServices;
using BrightCare.Service.Interface.Agency.MasterServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class MasterServiceController : BaseController
    {
        private readonly IMasterServices iMasterServices;
        public MasterServiceController(IMasterServices iMasterServices)
        {
            this.iMasterServices = iMasterServices;
        }

        [HttpGet]
        public ActionResult GetMasterService()
        {
            return Ok(iMasterServices.GetMasterService(GetToken(HttpContext)));
        }

        [HttpPost]
        public ActionResult SaveMasterService(MasterServicesDTO masterServicesModel)
        {
            return Ok(iMasterServices.AddUpdateMasterService(masterServicesModel, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteMasterService(int Id)
        {
            return (iMasterServices.DeleteMasterService(Id, GetToken(HttpContext)));
        }

    }
}
