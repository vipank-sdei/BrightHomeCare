using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.MasterAllergyReaction;
using BrightCare.Service.Interface.Agency.MasterAllergyReaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class MasterAllergyReactionController : BaseController
    {
        private readonly IMasterAllergyReactionService _masterAllergyReactionService;
        public MasterAllergyReactionController(IMasterAllergyReactionService _masterAllergyReactionService)
        {
            this._masterAllergyReactionService = _masterAllergyReactionService;
        }


        [HttpGet]
        public ActionResult GetMasterAllergyReaction()
        {
            return Ok(_masterAllergyReactionService.GetMasterAllergyReaction(GetToken(HttpContext)));
        }


        [HttpPost]
        public ActionResult SaveMasterAllergyReaction(MasterAllergyReactionDTO masterAllergyReactionDTO)
        {
            return Ok(_masterAllergyReactionService.AddUpdateMasterAllergyReaction(masterAllergyReactionDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteMasterAllergy(int Id)
        {
            return (_masterAllergyReactionService.DeleteMasterAllergyReaction(Id, GetToken(HttpContext)));
        }
    }
}
