using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency;
using BrightCare.Entity.Agency;
using BrightCare.Service.Interface.Agency.Patients;
using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Repository.Interface.Agency.Patients;
using System.Linq;
using HC.Common;
using static BrightCare.Common.Enums.CommonEnum;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace BrightCare.Service.Agency.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository iPatientRepository;
        private readonly IMapper _mapper;
        private JsonModel response = new JsonModel();

        public PatientService(IPatientRepository iPatientRepository, IMapper mapper)
        {
            this.iPatientRepository = iPatientRepository;
            this._mapper = mapper;
        }
        public JsonModel CreateUpdatePatient(PatientDTO patientDTO, TokenModel token)
        {
            token.OrganizationID = 2;
            token.UserID = 1;

            Patients patient = null;
            // check user already exist or not
            patient = iPatientRepository.CheckExistingPatient<Patients>(patientDTO.Email, patientDTO.MRN, patientDTO.Id, token).FirstOrDefault();
            if (patient != null)
            {
                //response
                response.data = new object();
                response.Message = StatusMessage.PatientAlreadyExist;
                response.StatusCode = 422;
                return response;
            }
            // Encrypt the fields data
            PHIEncryptedDTO pHIEncryptedModel = iPatientRepository.GetEncryptedPHIData<PHIEncryptedDTO>(patientDTO.FirstName, patientDTO.MiddleName, patientDTO.LastName, patientDTO.DOB != null ? patientDTO.DOB.ToString("yyyy-MM-dd HH:mm:ss.fffffff") : null, patientDTO.Email, patientDTO.SSN, patientDTO.MRN, null, null, null, null, null, null, null).FirstOrDefault();
            // save client
            if (patientDTO.Id == 0)
            {
                patient = new Patients();
                PatientMapToEntity(patientDTO, patient, pHIEncryptedModel, token, "add");
                iPatientRepository.Create(patient);
                iPatientRepository.SaveChanges();
                response.Message = StatusMessage.ClientCreated;
                response.StatusCode = (int)HttpStatusCodes.OK;
            }
            // update client
            else {
                patient = iPatientRepository.GetFirstOrDefault(a => a.Id == patientDTO.Id && a.IsDeleted == false && a.IsActive == true);
               // Map DTO Model to entity
                PatientMapToEntity(patientDTO, patient, pHIEncryptedModel, token, "update");
                iPatientRepository.Update(patient);
                iPatientRepository.SaveChanges();
                response.data = patientDTO;
                response.Message = StatusMessage.ClientUpdated;
                response.StatusCode = (int)HttpStatusCodes.OK;
            }
            return response;
        }

        private void PatientMapToEntity(PatientDTO patientDemographics, Patients patients, PHIEncryptedDTO pHIEncryptedModel, TokenModel token, string action)
        {
            //Mapping of Entries
            patients.MRN = pHIEncryptedModel.MRN;
            patients.FirstName = pHIEncryptedModel.FirstName;
            patients.MiddleName = pHIEncryptedModel.MiddleName;
            patients.LastName = pHIEncryptedModel.LastName;
            patients.DOB = pHIEncryptedModel.DateOfBirth;
            patients.SSN = pHIEncryptedModel.SSN;
            patients.Email = pHIEncryptedModel.EmailAddress;
            patients.Gender = patientDemographics.Gender;
            patients.OptOut = patientDemographics.OptOut;
            patients.PrimaryProvider = patientDemographics.PrimaryProvider;
            patients.RenderingProviderID = patientDemographics.RenderingProviderID;
            patients.LocationID = patientDemographics.LocationID;
            patients.IsPortalRequired = patientDemographics.IsPortalRequired;
            patients.IsPortalActivate = patientDemographics.IsPortalActivate;
            patients.EmergencyContactFirstName = patientDemographics.EmergencyContactFirstName;
            patients.EmergencyContactLastName = patientDemographics.EmergencyContactLastName;
            patients.EmergencyContactPhone = patientDemographics.EmergencyContactPhone;
            patients.PhotoBase64 = patientDemographics.PhotoBase64;
            if (action == "add")
            {
                patients.CreatedBy = token.UserID != 0 ? token.UserID : (int?)null;
                patients.CreatedDate = DateTime.UtcNow;
                patients.IsDeleted = false;
                patients.IsActive = true;
                patients.OrganizationID = token.OrganizationID;
                patients.LocationID = patients.LocationID != 0 ? patients.LocationID : token.LocationID;
            }
            else if (action == "update")
            {
                patients.UpdatedBy = token.UserID;
                patients.UpdatedDate = DateTime.UtcNow;
                //add new 
            }
            patients.Note = patientDemographics.Note;
        }

        public JsonModel DeletePatient(int id, TokenModel token)
        {
            token.OrganizationID = 2;
            token.UserID = 1;
            // Get Patient
           Patients patientEntity = iPatientRepository.GetFirstOrDefault(a => a.Id == id && a.IsDeleted == false && a.IsActive == true);
            if (patientEntity != null)
            {
                patientEntity.IsDeleted = true;
                patientEntity.DeletedBy = token.UserID;
                patientEntity.DeletedDate = DateTime.UtcNow;
                iPatientRepository.Update(patientEntity);
                iPatientRepository.SaveChanges();
                response.Message = StatusMessage.Delete;
                response.StatusCode = (int)HttpStatusCodes.OK;
            }
            else
            {
                response.Message = StatusMessage.NotFound;
                response.StatusCode = (int)HttpStatusCodes.NotFound;
            }
            return response;
        }

        public JsonModel GetPatient(int? id, TokenModel token)
        {
            token.OrganizationID = 2;
            PatientDTO patientDTO = new PatientDTO();
            // get single record
            if (id != null)
            {
                Patients patients = iPatientRepository.GetFirstOrDefault(a => a.Id == id && a.IsDeleted == false);

                if (patients != null)
                {
                    // Decrypt the data 
                    PHIDecryptedDTO pHIDecryptedDTO = iPatientRepository.GetDecryptedPHIData<PHIDecryptedDTO>(patients.FirstName, patients.MiddleName, patients.LastName, patients.DOB, patients.Email, patients.SSN, patients.MRN, null, null, null, null, null, null, null).FirstOrDefault();
                    MapPatientEntityToDTO(patientDTO, patients, pHIDecryptedDTO, token);
                    if (patientDTO != null && !string.IsNullOrEmpty(patientDTO.PhotoPath) && !string.IsNullOrEmpty(patientDTO.PhotoThumbnailPath))
                    {
                        patientDTO.PhotoPath = CommonMethods.CreateImageUrl(token.Request, ImagesPath.PatientPhotos, patientDTO.PhotoPath);
                        patientDTO.PhotoThumbnailPath = CommonMethods.CreateImageUrl(token.Request, ImagesPath.PatientThumbPhotos, patientDTO.PhotoThumbnailPath);
                    }
                    response = new JsonModel(patientDTO, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), StatusMessage.NotFound, (int)HttpStatusCodes.NotFound);
                }
            }
            // Get all ptients in decrypted form from db
            else
            {
                List<PatientDTO> patientModels = iPatientRepository.GetPatients<PatientDTO>(token).ToList();
                response = new JsonModel(patientModels, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
            }

            return response;          
        }

        private void MapPatientEntityToDTO(PatientDTO PatientDTO, Patients patients, PHIDecryptedDTO pHIDecryptedModel, TokenModel token)
        {
            
            PatientDTO.MRN = pHIDecryptedModel.MRN;
            PatientDTO.FirstName = pHIDecryptedModel.FirstName;
            PatientDTO.MiddleName = pHIDecryptedModel.MiddleName;
            PatientDTO.LastName = pHIDecryptedModel.LastName;
            PatientDTO.DOB = Convert.ToDateTime(pHIDecryptedModel.DateOfBirth);
            PatientDTO.SSN = pHIDecryptedModel.SSN;
            PatientDTO.Email = pHIDecryptedModel.EmailAddress;
            PatientDTO.Gender = patients.Gender;
            PatientDTO.OptOut = patients.OptOut != null ? (bool)patients.OptOut : false;
            PatientDTO.PrimaryProvider = patients.PrimaryProvider;
            PatientDTO.RenderingProviderID = patients.RenderingProviderID != null ? (int)patients.RenderingProviderID : 0;
            PatientDTO.PhotoPath = patients.PhotoPath;
            PatientDTO.PhotoThumbnailPath = patients.PhotoThumbnailPath;
            PatientDTO.LocationID = patients.LocationID;
            PatientDTO.IsPortalRequired = patients.IsPortalRequired;
            PatientDTO.Note = patients.Note;
            PatientDTO.EmergencyContactFirstName = patients.EmergencyContactFirstName;
            PatientDTO.EmergencyContactLastName = patients.EmergencyContactLastName;
            PatientDTO.EmergencyContactPhone = patients.EmergencyContactPhone;
            PatientDTO.PhotoBase64 = patients.PhotoBase64;
            PatientDTO.Id = patients.Id;
        }
    }
}
