using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.Document;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.Documnets;
using BrightCare.Repository.Interface.Agency.Organizations;
using BrightCare.Service.Interface.Agency.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using static BrightCare.Common.Enums.CommonEnum;
using HC.Common;

namespace BrightCare.Service.Agency.Documents
{
    public class UserDocumentService : IUserDocumentService
    {
        private readonly IUserDocumentRepository _documentRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public UserDocumentService(IUserDocumentRepository documentRepository, IMapper mapper, IOrganizationRepository _organizationRepository)
        {
            this._documentRepository = documentRepository;
            _mapper = mapper;
            this._organizationRepository = _organizationRepository;
        }

        public JsonModel CreateUpdateDocument(UserDocumentDTO documentDTO, TokenModel token)
        {
            token.OrganizationID = 2;
            token.UserID = 1;
            // get organisation name
            string organizationName = _organizationRepository.GetFirstOrDefault(a => a.Id == token.OrganizationID).OrganizationName;
            // add document
            if (documentDTO.Id == 0)
            {
                return UploadDocument(documentDTO, token, organizationName);
            }
            // update documnet
            else
            {
                return UpdateDocument(documentDTO, token, organizationName);
            }
        }

        public JsonModel DeleteUserDocument(int id, TokenModel token)
        {
            // get document by id
            UserDocument userDoc = _documentRepository.GetFirstOrDefault(a => a.Id == id && a.IsActive == true && a.IsDeleted == false);
            if (userDoc != null)
            {
                userDoc.IsDeleted = true;
                userDoc.DeletedDate = DateTime.UtcNow;
                _documentRepository.Update(userDoc);
                _documentRepository.SaveChanges();

                return new JsonModel()
                {
                    data = new object(),
                    Message = StatusMessage.DocumentDelete,
                    StatusCode = (int)HttpStatusCodes.NoContent
                };
            }
            else
            {
                return new JsonModel()
                {
                    data = new object(),
                    Message = StatusMessage.NotFound,
                    StatusCode = (int)HttpStatusCodes.NotFound
                };
            }
        }

        public JsonModel GetUserDocument(int Userid, string key, TokenModel token)
        {
            List<UserDocument> userDocument = _documentRepository.GetDocuments(Userid, key, token);
            List<UserDocumentResponseDTO> userDocumentDTO = _mapper.Map<List<UserDocumentResponseDTO>>(userDocument);
            return new JsonModel()
            {
                data = userDocumentDTO,
                Message = StatusMessage.FetchMessage,
                StatusCode = (int)HttpStatusCodes.OK
            };

        }

        public JsonModel GetUserDocumentById(int Documentid, TokenModel token)
        {
            // Get document by id
          UserDocument userDocument = _documentRepository.GetFirstOrDefault(a => a.Id == Documentid && a.IsDeleted == false && a.IsActive == true);
          UserDocumentResponseDTO userDocumentDTO = _mapper.Map<UserDocumentResponseDTO>(userDocument);
            return new JsonModel()
            {
                data = userDocumentDTO,
                Message = StatusMessage.FetchMessage,
                StatusCode = (int)HttpStatusCodes.OK
            };
        }

        public JsonModel UpdateDocument(UserDocumentDTO documentDTO, TokenModel token,string organizationName)
        {
            // Get record for update
            UserDocument userDoc = _documentRepository.GetFirstOrDefault(a => a.Id == documentDTO.Id && a.IsActive == true && a.IsDeleted == false); ;
            #region saveDoc
            if (userDoc != null)
            {
                string webRootPath = Directory.GetCurrentDirectory();
                string DirectoryUrl = userDoc.Key.ToUpper() == DocumentUserTypeEnum.PATIENT.ToString().ToUpper() ? ImagesPath.UploadClientDocuments : ImagesPath.UploadStaffDocuments;
                // path of directory
                string path = webRootPath + DirectoryUrl;
                // Delete exixting file from folder
                if (File.Exists(path + userDoc.UploadPath) && documentDTO.Base64.Count() > 0)
                {
                    File.Delete(path + userDoc.UploadPath);
                }
                foreach (var item in documentDTO.Base64)
                {
                    item.Value.Replace("\"", "");
                    string[] extensionArr = { "jpg", "jpeg", "png", "txt", "docx", "doc", "xlsx", "pdf", "pptx" };
                    //getting data from base64 url                    
                    string base64Data = item.Value.Replace("\"", "").Split(':')[0].ToString().Trim();
                    //getting extension of the image
                      string extension = item.Value.Replace("\"", "").Split(':')[1].ToString().Trim();

                    //string extension = "png";

                    //out from the loop if document extenstion not exist in list of extensionArr
                    if (!extensionArr.Contains(extension)) { goto Finish; }

                    //save folder
                    if (!Directory.Exists(webRootPath + DirectoryUrl))
                    if (!Directory.Exists(webRootPath + DirectoryUrl))
                    {
                        Directory.CreateDirectory(webRootPath + DirectoryUrl);
                    }

                    string fileName = organizationName + "_" + DateTime.UtcNow.TimeOfDay.ToString();

                    //update file name remove unsupported attr.
                    fileName = fileName.Replace(" ", "_").Replace(":", "_");

                    //create path for save location
                     path = path + fileName + "." + extension;

                    //convert files into base
                    Byte[] bytes = Convert.FromBase64String(base64Data);
                    //save int the directory
                    File.WriteAllBytes(path, bytes);

                    //create db path
                    userDoc.UploadPath = fileName + "." + extension;
                }
                userDoc.DocumentName = documentDTO.DocumentTitle;
                userDoc.Expiration = documentDTO.Expiration;
                userDoc.DocumentTypeId = documentDTO.DocumentTypeId;
                userDoc.UpdatedBy = token.UserID;
                userDoc.UpdatedDate = DateTime.UtcNow;
                _documentRepository.Update(userDoc);
                _documentRepository.SaveChanges();
                //save into db
                return new JsonModel()
                {
                    data = new object(),
                    Message = StatusMessage.DocumentUpdated,
                    StatusCode = (int)HttpStatusCodes.OK
                };
            }
            else
                goto Finish;
            #endregion
            Finish:;
            return new JsonModel()
            {
                data = new object(),
                Message = StatusMessage.InvaildFormat,
                StatusCode = (int)HttpStatusCodes.UnprocessedEntity
            };
        }


        public JsonModel UploadDocument(UserDocumentDTO documentDTO, TokenModel token,string organizationName)
        {
            List<UserDocument> userDocList = new List<UserDocument>();
            #region saveDoc
            // check for multiple documents
            foreach (var item in documentDTO.Base64)
            {
                UserDocument userDoc = new UserDocument();

                item.Value.Replace("\"", "");
                string[] extensionArr = { "jpg", "jpeg", "png", "txt", "docx", "doc", "xlsx", "pdf", "pptx" };
                //getting data from base64 url                    
                string base64Data = item.Value.Replace("\"", "").Split(':')[0].ToString().Trim();
                //getting extension of the image
                string extension = item.Value.Replace("\"", "").Split(':')[1].ToString().Trim();

                //string extension = "png";

                //out from the loop if document extenstion not exist in list of extensionArr
                if (!extensionArr.Contains(extension)) { goto Finish; }

                //create directory
                //string webRootPath = Directory.GetCurrentDirectory()+ "\\PatientDocuments";
                string webRootPath = Directory.GetCurrentDirectory();

                //save folder
                string DirectoryUrl = documentDTO.Key.ToUpper() == DocumentUserTypeEnum.PATIENT.ToString().ToUpper() ? ImagesPath.UploadClientDocuments : ImagesPath.UploadStaffDocuments;

                if (!Directory.Exists(webRootPath + DirectoryUrl))
                {
                    Directory.CreateDirectory(webRootPath + DirectoryUrl);
                }

                string fileName = organizationName + "_" + DateTime.UtcNow.TimeOfDay.ToString();

                //update file name remove unsupported attr.
                fileName = fileName.Replace(" ", "_").Replace(":", "_");

                //create path for save location
                string path = webRootPath + DirectoryUrl + fileName + "." + extension;

                //convert files into base
                Byte[] bytes = Convert.FromBase64String(base64Data);
                //save int the directory
                File.WriteAllBytes(path, bytes);

                //create db path
                //string uploadPath = @"/Documents/ClientDocuments/" + fileName + "." + extension;

                userDoc.CreatedBy = token.UserID;
                userDoc.CreatedDate = DateTime.UtcNow;
                userDoc.IsActive = true;
                userDoc.IsDeleted = false;
                userDoc.UserId = documentDTO.UserId;
                userDoc.DocumentName = documentDTO.DocumentTitle;
                userDoc.Expiration = documentDTO.Expiration;
                userDoc.OtherDocumentType = documentDTO.OtherDocumentType;
                userDoc.DocumentTypeId = documentDTO.DocumentTypeId;
                userDoc.CreatedDate = DateTime.UtcNow;
                userDoc.UploadPath = fileName + "." + extension;
                userDoc.Key = documentDTO.Key;
                userDocList.Add(userDoc);

            }
            //save into db
            _documentRepository.UploadDocument(userDocList);
            return new JsonModel()
            {
                data = new object(),
                Message = StatusMessage.DocumentUploaded,
                StatusCode = (int)HttpStatusCodes.OK
            };
        #endregion

        Finish:;
            return new JsonModel()
            {
                data = new object(),
                Message = StatusMessage.InvaildFormat,
                StatusCode = (int)HttpStatusCodes.UnprocessedEntity
            };

        }
    }
}
