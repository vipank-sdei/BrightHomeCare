using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrightCare.Service.Interface.Agency.MasterServices
{
    public interface IMasterServices 
    {
        JsonModel GetMasterService(TokenModel token);
        JsonModel AddUpdateMasterService(MasterServicesDTO masterServiceCodes, TokenModel token);
        bool DeleteMasterService(int Id, TokenModel token);
    }
}
