using BrightCare.Common;
using BrightCare.Entity.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Interface.Agency.Documnets
{
    public interface IUserDocumentRepository : IRepositoryBase<UserDocument>
    {
        void UploadDocument(List<UserDocument> userDocument);

        List<UserDocument> GetDocuments(int UserId, string Key, TokenModel token);

    }
}
