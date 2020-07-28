using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.MasterServiceTypes;
using BrightCare.Service.Interface.Agency.MasterServiceTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class MasterServiceTypeController : BaseController
    {

        private readonly IMasterServiceType _masterServiceType;
        public MasterServiceTypeController(IMasterServiceType _masterServiceType)
        {
            this._masterServiceType = _masterServiceType;
        }

        [HttpGet]
        public ActionResult GetMasterServiceType()
        {
            return Ok(_masterServiceType.GetMasterServiceType(GetToken(HttpContext)));
        }

        [HttpPost]
        public ActionResult SaveMasterServiceType(MasterServiceTypeDTO masterServiceTypeDTO)
        {
            return Ok(_masterServiceType.AddUpdateMasterServiceType(masterServiceTypeDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteMasterServiceTyp(int Id)
        {
            return (_masterServiceType.DeleteMasterServiceType(Id, GetToken(HttpContext)));
        }

    }
}
