using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.MasterAllergy;
using BrightCare.Service.Interface.Agency.MasterAllergy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class MasterAllergyController : BaseController
    {
        private readonly IMasterAllergyService _masterAllergyService;
        public MasterAllergyController(IMasterAllergyService _masterAllergyService)
        {
            this._masterAllergyService = _masterAllergyService;
        }


        [HttpGet]
        public ActionResult GetMasterAllergy()
        {
            return Ok(_masterAllergyService.GetMasterAllergy(GetToken(HttpContext)));
        }


        [HttpPost]
        public ActionResult SaveMasterAllergy(MasterAllergyDTO masterAllergyDTO)
        {
            return Ok(_masterAllergyService.AddUpdateMasterAllergy(masterAllergyDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteMasterAllergy(int Id)
        {
            return (_masterAllergyService.DeleteMasterAllergy(Id, GetToken(HttpContext)));
        }
    }
}
