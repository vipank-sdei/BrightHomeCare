using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.DiagnosisCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.DiagnosisCodes
{
    public interface IDiagnosisCodesService
    {
        JsonModel GetDiagnosisCodes(TokenModel token);
        JsonModel AddUpdateDiagnosisCodes(DiagnosisCodesDTO diagnosisCodesDTO, TokenModel token);
        bool DeleteDiagnosisCodes(int Id, TokenModel token);
    }
}
