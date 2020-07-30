using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterAllergy;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.MasterAllergy;
using BrightCare.Service.Interface.Agency.MasterAllergy;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.MasterAllergy
{
    public class MasterAllergyService : IMasterAllergyService
    {
        private readonly IMasterAllergyRepository _masterAllergyRepository;
        private readonly IMapper _mapper;

        public MasterAllergyService(IMasterAllergyRepository _masterAllergyRepository, IMapper mapper)
        {
            this._masterAllergyRepository = _masterAllergyRepository;
            _mapper = mapper;

        }

        public JsonModel GetMasterAllergy(TokenModel token)
        {
            List<MasterAllergyDTO> masterAllergyDTO = new List<MasterAllergyDTO>();
            List<MasterAllergies> masterAllergies = _masterAllergyRepository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            masterAllergyDTO = _mapper.Map<List<MasterAllergyDTO>>(masterAllergies); // Mapping

            return new JsonModel(masterAllergyDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }

        public JsonModel AddUpdateMasterAllergy(MasterAllergyDTO masterAllergyDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            MasterAllergies masterAllergiesEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (masterAllergyDTO.Id == 0 || masterAllergyDTO.Id == null)
            {
                masterAllergiesEntity = _mapper.Map<MasterAllergies>(masterAllergyDTO);
                masterAllergiesEntity.OrganizationId = 2; // token.OrganizationID;
                masterAllergiesEntity.CreatedBy = 2;// token.UserID;
                masterAllergiesEntity.CreatedDate = CurrentDate;
                masterAllergiesEntity.IsActive = true;
                masterAllergiesEntity.IsDeleted = false;
                _masterAllergyRepository.Create(masterAllergiesEntity);
                _masterAllergyRepository.SaveChanges();
            }

            else
            {
                MasterAllergies masterAllergies = _masterAllergyRepository.Get(l => l.Id == masterAllergyDTO.Id && l.OrganizationId == 2); // token.OrganizationID);
                masterAllergies.UpdatedBy = 2; // token.UserID;
                masterAllergies.UpdatedDate = CurrentDate;
                masterAllergies.AllergyType = masterAllergyDTO.AllergyType;
                _masterAllergyRepository.Update(masterAllergies);
                _masterAllergyRepository.SaveChanges();
            }

            return Result;
        }

        public bool DeleteMasterAllergy(int Id, TokenModel token)
        {
            MasterAllergies masterAllergiesEntity = _masterAllergyRepository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            masterAllergiesEntity.IsDeleted = true;
            masterAllergiesEntity.IsActive = false;
            masterAllergiesEntity.DeletedBy = 2;// token.UserID;
            masterAllergiesEntity.DeletedDate = DateTime.UtcNow;
            _masterAllergyRepository.SaveChanges();

            return true;
        }
    }
}
