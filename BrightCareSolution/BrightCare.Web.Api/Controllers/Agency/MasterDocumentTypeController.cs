using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.MasterDocumentType;
using BrightCare.Service.Interface.Agency.MasterDocumentType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class MasterDocumentTypeController : BaseController
    {
        private readonly IMasterDocumentTypeService _masterDocuentTypeService;
        public MasterDocumentTypeController(IMasterDocumentTypeService _masterDocuentTypeService)
        {
            this._masterDocuentTypeService = _masterDocuentTypeService;
        }

        [HttpGet]
        public ActionResult GetMasterDocumentType()
        {
            return Ok(_masterDocuentTypeService.GetMasterDocumentType(GetToken(HttpContext)));
        }


        [HttpPost]
        public ActionResult SaveMasterDocumentType(MasterDocumentTypeDTO masterDocumentTypeDTO)
        {
            return Ok(_masterDocuentTypeService.AddUpdateMasterDocumentType(masterDocumentTypeDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteMasterDocumentType(int Id)
        {
            return (_masterDocuentTypeService.DeleteMasterDocumentType(Id, GetToken(HttpContext)));
        }
    }
}
