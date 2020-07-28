using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterDocumentType;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.MasterDocumentType;
using BrightCare.Service.Interface.Agency.MasterDocumentType;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.MasterDocumentType
{
    public class MasterDocumentTypeService: IMasterDocumentTypeService
    {
        private readonly IMasterDocumentTypeRepository _masterDocumentTypeRepository;
        private readonly IMapper _mapper;

        public MasterDocumentTypeService(IMasterDocumentTypeRepository _masterDocumentTypeRepository, IMapper mapper)
        {
            this._masterDocumentTypeRepository = _masterDocumentTypeRepository;
            _mapper = mapper;

        }

        public JsonModel GetMasterDocumentType(TokenModel token)
        {
            List<MasterDocumentTypeDTO> masterDocumentTypeDTOs = new List<MasterDocumentTypeDTO>();
            List<MasterDocumentTypes> masterDocumentTypesEntity = _masterDocumentTypeRepository.GetAll(l => l.IsDeleted == false && l.OrganizationID == 2).ToList();// token.OrganizationID);
            masterDocumentTypeDTOs = _mapper.Map<List<MasterDocumentTypeDTO>>(masterDocumentTypesEntity); // Mapping

            return new JsonModel(masterDocumentTypeDTOs, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }


        public JsonModel AddUpdateMasterDocumentType(MasterDocumentTypeDTO masterDocumentTypeDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            MasterDocumentTypes masterDocumentTypesEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (masterDocumentTypeDTO.Id == 0 || masterDocumentTypeDTO.Id == null)
            {
                masterDocumentTypesEntity = _mapper.Map<MasterDocumentTypes>(masterDocumentTypeDTO);
                masterDocumentTypesEntity.OrganizationID = 2; // token.OrganizationID;
                masterDocumentTypesEntity.CreatedBy = 2;// token.UserID;
                masterDocumentTypesEntity.CreatedDate = CurrentDate;
                masterDocumentTypesEntity.IsActive = true;
                _masterDocumentTypeRepository.Create(masterDocumentTypesEntity);
                _masterDocumentTypeRepository.SaveChanges();
            }

            else
            {
                MasterDocumentTypes masterDocumentTypes = _masterDocumentTypeRepository.Get(l => l.Id == masterDocumentTypeDTO.Id && l.OrganizationID == 2); // token.OrganizationID);
                masterDocumentTypes.UpdatedBy = 2; // token.UserID;
                masterDocumentTypes.UpdatedDate = CurrentDate;
                masterDocumentTypes.Type = masterDocumentTypeDTO.Type;
                _masterDocumentTypeRepository.Update(masterDocumentTypes);
                _masterDocumentTypeRepository.SaveChanges();
            }

            return Result;
        }

        public bool DeleteMasterDocumentType(int Id, TokenModel token)
        {
            MasterDocumentTypes masterDocumentTypes = _masterDocumentTypeRepository.Get(l => l.Id == Id && l.OrganizationID == 2);// token.OrganizationID);
            masterDocumentTypes.IsDeleted = true;
            masterDocumentTypes.IsActive = false;
            masterDocumentTypes.DeletedBy = 2;// token.UserID;
            masterDocumentTypes.DeletedDate = DateTime.UtcNow;
            _masterDocumentTypeRepository.SaveChanges();

            return true;
        }
    }
}
