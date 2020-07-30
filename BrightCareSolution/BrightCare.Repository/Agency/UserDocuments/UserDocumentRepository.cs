using BrightCare.Common;
using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Documnets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightCare.Repository.Agency.Documents
{
   public class UserDocumentRepository : RepositoryBase<UserDocument>, IUserDocumentRepository
    {
        private HCOrganizationContext _context;
        public UserDocumentRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }

        public void UploadDocument(List<UserDocument> userDocument)
        {
            _context.UserDocuments.AddRange(userDocument);
            _context.SaveChanges();
        }

        public List<UserDocument> GetDocuments(int UserId, string Key,TokenModel token)
        {
         List<UserDocument> document =  _context.UserDocuments.Where(a => a.UserId == UserId && a.Key == Key && a.IsActive == true && a.IsDeleted == false).Include("MasterDocumentTypes").ToList();
            return document;
        }

       
    }
}
