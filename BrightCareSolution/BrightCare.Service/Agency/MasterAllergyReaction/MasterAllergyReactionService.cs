using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.MasterAllergyReaction;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.MasterAllergyReaction;
using BrightCare.Service.Interface.Agency.MasterAllergyReaction;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.MasterAllergyReaction
{
    public class MasterAllergyReactionService : IMasterAllergyReactionService
    {
        private readonly IMasterAllergyReactionRepository _masterAllergyReactionRepository;
        private readonly IMapper _mapper;

        public MasterAllergyReactionService(IMasterAllergyReactionRepository _masterAllergyReactionRepository, IMapper mapper)
        {
            this._masterAllergyReactionRepository = _masterAllergyReactionRepository;
            _mapper = mapper;

        }


        public JsonModel GetMasterAllergyReaction(TokenModel token)
        {
            List<MasterAllergyReactionDTO> masterAllergyReactionDTO = new List<MasterAllergyReactionDTO>();
            List<MasterAllergiesReaction> masterAllergiesReaction = _masterAllergyReactionRepository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            masterAllergyReactionDTO = _mapper.Map<List<MasterAllergyReactionDTO>>(masterAllergiesReaction); // Mapping

            return new JsonModel(masterAllergyReactionDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }

        public JsonModel AddUpdateMasterAllergyReaction(MasterAllergyReactionDTO masterAllergyReactionDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            MasterAllergiesReaction masterAllergiesReactionEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (masterAllergyReactionDTO.Id == 0 || masterAllergyReactionDTO.Id == null)
            {
                masterAllergiesReactionEntity = _mapper.Map<MasterAllergiesReaction>(masterAllergyReactionDTO);
                masterAllergiesReactionEntity.OrganizationId = 2; // token.OrganizationID;
                masterAllergiesReactionEntity.CreatedBy = 2;// token.UserID;
                masterAllergiesReactionEntity.CreatedDate = CurrentDate;
                masterAllergiesReactionEntity.IsActive = true;
                masterAllergiesReactionEntity.IsDeleted = false;
                _masterAllergyReactionRepository.Create(masterAllergiesReactionEntity);
                _masterAllergyReactionRepository.SaveChanges();
            }

            else
            {
                MasterAllergiesReaction masterAllergiesReaction = _masterAllergyReactionRepository.Get(l => l.Id == masterAllergyReactionDTO.Id && l.OrganizationId == 2); // token.OrganizationID);
                masterAllergiesReaction.UpdatedBy = 2; // token.UserID;
                masterAllergiesReaction.UpdatedDate = CurrentDate;
                masterAllergiesReaction.ReactionType = masterAllergyReactionDTO.ReactionType;
                _masterAllergyReactionRepository.Update(masterAllergiesReaction);
                _masterAllergyReactionRepository.SaveChanges();
            }

            return Result;
        }

        public bool DeleteMasterAllergyReaction(int Id, TokenModel token)
        {
            MasterAllergiesReaction masterAllergiesReactionEntity = _masterAllergyReactionRepository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            masterAllergiesReactionEntity.IsDeleted = true;
            masterAllergiesReactionEntity.IsActive = false;
            masterAllergiesReactionEntity.DeletedBy = 2;// token.UserID;
            masterAllergiesReactionEntity.DeletedDate = DateTime.UtcNow;
            _masterAllergyReactionRepository.SaveChanges();

            return true;
        }
    }
}
