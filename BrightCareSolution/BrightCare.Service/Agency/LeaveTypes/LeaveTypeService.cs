using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.LeaveTypes;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.LeaveTypes;
using BrightCare.Service.Interface.Agency.LeaveTypes;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.LeaveTypes
{
    public class LeaveTypeService: ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveTypeService(ILeaveTypeRepository _leaveTypeRepository, IMapper mapper)
        {
            this._leaveTypeRepository = _leaveTypeRepository;
            _mapper = mapper;

        }

        public JsonModel GetLeaveType(TokenModel token)
        {
            List<LeaveTypeDTO> leaveTypeDTO = new List<LeaveTypeDTO>();
            List<LeaveType> leaveTypes = _leaveTypeRepository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            leaveTypeDTO = _mapper.Map<List<LeaveTypeDTO>>(leaveTypes); // Mapping

            return new JsonModel(leaveTypeDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }

        public JsonModel AddUpdateLeaveType(LeaveTypeDTO leaveTypeDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            LeaveType leaveTypeEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (leaveTypeDTO.Id == 0 || leaveTypeDTO.Id == null)
            {
                leaveTypeEntity = _mapper.Map<LeaveType>(leaveTypeDTO);
                leaveTypeEntity.OrganizationId = 2; // token.OrganizationID;
                leaveTypeEntity.CreatedBy = 2;// token.UserID;
                leaveTypeEntity.CreatedDate = CurrentDate;
                leaveTypeEntity.IsActive = true;
                leaveTypeEntity.IsDeleted = false;
                _leaveTypeRepository.Create(leaveTypeEntity);
                _leaveTypeRepository.SaveChanges();
            }

            else
            {
                LeaveType leaveType = _leaveTypeRepository.Get(l => l.Id == leaveTypeDTO.Id && l.OrganizationId == 2); // token.OrganizationID);
                leaveType.UpdatedBy = 2; // token.UserID;
                leaveType.UpdatedDate = CurrentDate;
                leaveType.Type = leaveTypeDTO.Type;
                _leaveTypeRepository.Update(leaveType);
                _leaveTypeRepository.SaveChanges();
            }

            return Result;
        }


        public bool DeletLeaveType(int Id, TokenModel token)
        {
            LeaveType leaveTypeEntity = _leaveTypeRepository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            leaveTypeEntity.IsDeleted = true;
            leaveTypeEntity.IsActive = false;
            leaveTypeEntity.DeletedBy = 2;// token.UserID;
            leaveTypeEntity.DeletedDate = DateTime.UtcNow;
            _leaveTypeRepository.SaveChanges();

            return true;
        }
    }
}
