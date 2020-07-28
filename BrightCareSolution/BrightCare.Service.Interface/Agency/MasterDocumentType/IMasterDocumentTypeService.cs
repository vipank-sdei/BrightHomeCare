using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterDocumentType;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.MasterDocumentType
{
    public interface IMasterDocumentTypeService
    {
        JsonModel GetMasterDocumentType(TokenModel token);
        JsonModel AddUpdateMasterDocumentType(MasterDocumentTypeDTO masterDocumentTypeDTO, TokenModel token);
        bool DeleteMasterDocumentType(int Id, TokenModel token);
    }
}
