using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterServiceTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.MasterServiceTypes
{
    public interface IMasterServiceType 
    {
        JsonModel GetMasterServiceType(TokenModel token);
        JsonModel AddUpdateMasterServiceType(MasterServiceTypeDTO masterServiceTypeDTO, TokenModel token);
        bool DeleteMasterServiceType(int Id, TokenModel token);
    }
}
