using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.Documents
{
    public interface IUserDocumentService
    {
        JsonModel CreateUpdateDocument(UserDocumentDTO documentDTO, TokenModel token);

        JsonModel DeleteUserDocument(int id, TokenModel token);

        JsonModel GetUserDocument(int Userid, string key, TokenModel token);

        JsonModel GetUserDocumentById(int Documentid, TokenModel token);


    }
}
