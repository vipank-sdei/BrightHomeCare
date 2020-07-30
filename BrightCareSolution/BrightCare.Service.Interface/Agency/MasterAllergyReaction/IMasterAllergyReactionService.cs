using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterAllergyReaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.MasterAllergyReaction
{
    public interface IMasterAllergyReactionService
    {
        JsonModel GetMasterAllergyReaction(TokenModel token);
        JsonModel AddUpdateMasterAllergyReaction(MasterAllergyReactionDTO masterAllergyReactionDTO, TokenModel token);
        bool DeleteMasterAllergyReaction(int Id, TokenModel token);
    }
}
