using BrightCare.Dtos.Agency.MasterDocumentType;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency.Document
{
   public class UserDocumentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Dictionary<string, string> Base64 { get; set; }
       // public List<IFormFile> UploadFile { get; set; }
        public string DocumentTitle { get; set; }
        public string Url { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        //public int? DocumentTypeIdStaff { get; set; }
        //public string DocumentTypeNameStaff { get; set; }
        public DateTime? Expiration { get; set; }
        public string Extenstion { get; set; }
        public string OtherDocumentType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Key { get; set; }
    }

    public class UserDocumentResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public string DocumentTitle { get; set; }
        public string Url { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        
        public DateTime? Expiration { get; set; }
        public string Extenstion { get; set; }
        public string OtherDocumentType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Key { get; set; }

        public MasterDocumentTypeDTO MasterDocumentTypes { get; set; }
    }
}
