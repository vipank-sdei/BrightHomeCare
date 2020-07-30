using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterAllergy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.MasterAllergy
{
    public interface IMasterAllergyService
    {
        JsonModel GetMasterAllergy(TokenModel token);
        JsonModel AddUpdateMasterAllergy(MasterAllergyDTO masterAllergyDTO, TokenModel token);
        bool DeleteMasterAllergy(int Id, TokenModel token);
    }
}
