using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.Staff;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.Staff;
using BrightCare.Service.Interface.Agency.Staffs;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BrightCare.Service.Agency.Staff
{
   public class StaffExperienceService : IStaffExperienceService
    {
        private readonly IStaffExperienceRepository iStaffExperienceRepository;
         private readonly IMapper _mapper;
        private JsonModel _response;
        public StaffExperienceService(IStaffExperienceRepository iStaffExperienceRepository, IMapper mapper)
        {
            this.iStaffExperienceRepository = iStaffExperienceRepository;
            _mapper = mapper;
        }

        public JsonModel CreateUpdateStaffExperience(StaffExperienceRequestDTO staffExperienceRequestDTO, TokenModel token)
        {
            token.UserID = 1;
            int result = 0;
            StaffExperience experience;
            staffExperienceRequestDTO.staffExperiences.ForEach(expModel =>
            {
                experience = new StaffExperience();
                expModel.StaffId = staffExperienceRequestDTO.staffId;
                if (expModel.Id != 0)
                {
                    experience = iStaffExperienceRepository.GetFirstOrDefault(a => a.Id == expModel.Id && a.IsActive == true && a.IsDeleted == false);
                    experience.UpdatedDate = DateTime.UtcNow;
                    experience.UpdatedBy = token.UserID;
                }
                else
                {
                    experience.CreatedBy = token.UserID;
                    experience.CreatedDate = DateTime.UtcNow;
                    experience.IsActive = true;
                    experience.IsDeleted = false;
                }
                _mapper.Map(expModel, experience);
                if (experience.Id > 0)
                {
                   iStaffExperienceRepository.Update(experience);
                }
                else
                {
                   iStaffExperienceRepository.Create(experience);
                }
                iStaffExperienceRepository.SaveChanges();
                    result++;
            });
            if (result > 0)
            {
                _response = new JsonModel(new object(), StatusMessage.ExperienceSaved, (int)HttpStatusCode.OK);
            }
            else
            {
                _response = new JsonModel(new object(), StatusMessage.ExperienceSaved, (int)HttpStatusCode.BadRequest);
            }
                return _response;
        }

        public JsonModel GetStaffExperienceByUserID(int staffId,TokenModel tokenModel)
        {
            List<StaffExperience> staffExperiences = iStaffExperienceRepository.GetAll(a => a.StaffId == staffId && a.IsActive == true).ToList();
            if (staffExperiences != null && staffExperiences.Count > 0)
            {
                List<StaffExperienceDTO> staffExperienceDTO =   _mapper.Map<List<StaffExperienceDTO>>(staffExperiences);
                _response = new JsonModel(staffExperienceDTO, StatusMessage.FetchMessage, (int)HttpStatusCode.OK);
            }
            else
            {
                _response = new JsonModel(new object(), StatusMessage.NotFound, (int)HttpStatusCode.NotFound);
            }
            return _response;
        }

        public JsonModel GetStaffExperienceByID(int Id, TokenModel tokenModel)
        {
            StaffExperience staffExperiences = iStaffExperienceRepository.GetFirstOrDefault(a => a.Id == Id && a.IsActive == true);
            if (staffExperiences != null)
            {
               StaffExperienceDTO staffExperienceDTO = _mapper.Map<StaffExperienceDTO>(staffExperiences);
                _response = new JsonModel(staffExperienceDTO, StatusMessage.ExperienceSaved, (int)HttpStatusCode.OK);
            }
            else
            {
                _response = new JsonModel(new object(), StatusMessage.NotFound, (int)HttpStatusCode.NotFound);
            }
            return _response;
        }

        public JsonModel DeleteStaffExperience(int id, TokenModel token)
        {
            token.UserID = 1;
            StaffExperience staffExperiences = iStaffExperienceRepository.GetFirstOrDefault(a => a.Id == id && a.IsActive == true);
            if (staffExperiences != null)
            {
                staffExperiences.IsDeleted = true;
                staffExperiences.DeletedBy = token.UserID;
                staffExperiences.DeletedDate = DateTime.UtcNow;
                _response = new JsonModel(new object(), StatusMessage.DeleteMessage, (int)HttpStatusCode.OK);
            }
            else
            {
                _response = new JsonModel(new object(), StatusMessage.NotFound, (int)HttpStatusCode.NotFound);
            }
            return _response;
        }
    }
}
