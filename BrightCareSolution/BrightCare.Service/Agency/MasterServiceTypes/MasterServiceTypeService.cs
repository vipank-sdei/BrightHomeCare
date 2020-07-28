using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Common.Service;
using BrightCare.Dtos.Agency.MasterServiceTypes;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.MasterServiceTypes;
using BrightCare.Service.Interface.Agency.MasterServiceTypes;
using HC.Common.HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.MasterServiceTypes
{
    public class MasterServiceTypeService : BaseService, IMasterServiceType
    {
        private readonly IMasterServiceTypeRepository _masterServiceTypeRepository;
        private readonly IMapper _mapper;

        public MasterServiceTypeService(IMasterServiceTypeRepository _masterServiceTypeRepository, IMapper mapper)
        {
            this._masterServiceTypeRepository = _masterServiceTypeRepository;
            _mapper = mapper;

        }

        public JsonModel GetMasterServiceType(TokenModel token)
        {
            List<MasterServiceTypeDTO> masterServiceTypeDTO = new List<MasterServiceTypeDTO>();
            List<MasterServiceType> masterServiceType = _masterServiceTypeRepository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            masterServiceTypeDTO = _mapper.Map<List<MasterServiceTypeDTO>>(masterServiceType); // Mapping

            return new JsonModel(masterServiceTypeDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }



        public JsonModel AddUpdateMasterServiceType(MasterServiceTypeDTO masterServiceTypeDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            MasterServiceType masterServiceTypeEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (masterServiceTypeDTO.Id == 0 || masterServiceTypeDTO.Id == null)
            {
                masterServiceTypeEntity = _mapper.Map<MasterServiceType>(masterServiceTypeDTO);
                masterServiceTypeEntity.OrganizationId = 2; // token.OrganizationID;
                masterServiceTypeEntity.CreatedBy = 2;// token.UserID;
                masterServiceTypeEntity.CreatedDate = CurrentDate;
                masterServiceTypeEntity.IsActive = true;
                _masterServiceTypeRepository.Create(masterServiceTypeEntity);
                _masterServiceTypeRepository.SaveChanges();
            }

            else
            {
                MasterServiceType masterServiceType = _masterServiceTypeRepository.Get(l => l.Id == masterServiceTypeDTO.Id && l.OrganizationId == 2); // token.OrganizationID);
                masterServiceType.UpdatedBy = 2; // token.UserID;
                masterServiceType.UpdatedDate = CurrentDate;
                masterServiceType.ServiceType = masterServiceTypeDTO.ServiceType;
                _masterServiceTypeRepository.Update(masterServiceType);
                _masterServiceTypeRepository.SaveChanges();
            }

            return Result;
        }


        public bool DeleteMasterServiceType(int Id, TokenModel token)
        {
            MasterServiceType masterServiceType = _masterServiceTypeRepository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            masterServiceType.IsDeleted = true;
            masterServiceType.IsActive = false;
            masterServiceType.DeletedBy = 2;// token.UserID;
            masterServiceType.DeletedDate = DateTime.UtcNow;
            _masterServiceTypeRepository.SaveChanges();

            return true;
        }
    }
}
