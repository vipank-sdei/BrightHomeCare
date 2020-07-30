using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.DiagnosisCodes;
using BrightCare.Service.Interface.Agency.DiagnosisCodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class DiagnosisCodesController : BaseController
    {
        private readonly IDiagnosisCodesService _diagnosisTypesService;
        public DiagnosisCodesController(IDiagnosisCodesService _diagnosisTypesService)
        {
            this._diagnosisTypesService = _diagnosisTypesService;
        }

        [HttpGet]
        public ActionResult GetDiagnosisCodes()
        {
            return Ok(_diagnosisTypesService.GetDiagnosisCodes(GetToken(HttpContext)));
        }


        [HttpPost]
        public ActionResult SaveDiagnosisCodes(DiagnosisCodesDTO diagnosisCodesDTO)
        {
            return Ok(_diagnosisTypesService.AddUpdateDiagnosisCodes(diagnosisCodesDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public bool DeleteDiagnosisCodes(int Id)
        {
            return (_diagnosisTypesService.DeleteDiagnosisCodes(Id, GetToken(HttpContext)));
        }
    }
}
