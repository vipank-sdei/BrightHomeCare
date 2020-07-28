using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.Patients
{
    public interface IPatientService 
    {
        JsonModel CreateUpdatePatient(PatientDTO patientDTO, TokenModel token);

        JsonModel DeletePatient(int id, TokenModel token);

        JsonModel GetPatient(int? id, TokenModel token);
    }
}
