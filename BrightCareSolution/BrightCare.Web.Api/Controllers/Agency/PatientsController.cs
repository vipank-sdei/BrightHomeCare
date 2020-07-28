using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrightCare.Service.Interface.Agency.Patients;
using BrightCare.Dtos.Agency;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class PatientsController : BaseController
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        [HttpPost]
        public ActionResult CreateUpdatePatient(PatientDTO patientDTO)
        {
            return Ok(_patientService.CreateUpdatePatient(patientDTO, GetToken(HttpContext)));
        }

        [HttpDelete]
        public ActionResult DeletePatient(int id)
        {
            return Ok(_patientService.DeletePatient(id, GetToken(HttpContext)));
        }

        [HttpGet]
        public ActionResult GetPatient(int? id)
        {
            return Ok(_patientService.GetPatient(id, GetToken(HttpContext)));
        }
    }
}
