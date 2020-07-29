using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.LeaveReasons;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.LeaveReasons;
using BrightCare.Service.Interface.Agency.LeaveReasons;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.LeaveReasons
{
    public class LeaveReasonService: ILeaveReasonService
    {
        private readonly ILeaveReasonReapository _leaveReasonReapository;
        private readonly IMapper _mapper;

        public LeaveReasonService(ILeaveReasonReapository _leaveReasonReapository, IMapper mapper)
        {
            this._leaveReasonReapository = _leaveReasonReapository;
            _mapper = mapper;

        }

        public JsonModel GetLeaveReason(TokenModel token)
        {
            List<LeaveReasonDTO> leaveTypeDTO = new List<LeaveReasonDTO>();
            List<LeaveReason> leaveReasons = _leaveReasonReapository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            leaveTypeDTO = _mapper.Map<List<LeaveReasonDTO>>(leaveReasons); // Mapping

            return new JsonModel(leaveTypeDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }

        public JsonModel AddUpdateLeaveReason(LeaveReasonDTO leaveReasonDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            LeaveReason leaveReasonEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (leaveReasonDTO.Id == 0 || leaveReasonDTO.Id == null)
            {
                leaveReasonEntity = _mapper.Map<LeaveReason>(leaveReasonDTO);
                leaveReasonEntity.OrganizationId = 2; // token.OrganizationID;
                leaveReasonEntity.CreatedBy = 2;// token.UserID;
                leaveReasonEntity.CreatedDate = CurrentDate;
                leaveReasonEntity.IsActive = true;
                leaveReasonEntity.IsDeleted = false;
                _leaveReasonReapository.Create(leaveReasonEntity);
                _leaveReasonReapository.SaveChanges();
            }

            else
            {
                LeaveReason leaveReason = _leaveReasonReapository.Get(l => l.Id == leaveReasonDTO.Id && l.OrganizationId == 2); // token.OrganizationID);
                leaveReason.UpdatedBy = 2; // token.UserID;
                leaveReason.UpdatedDate = CurrentDate;
                leaveReason.Type = leaveReasonDTO.Type;
                _leaveReasonReapository.Update(leaveReason);
                _leaveReasonReapository.SaveChanges();
            }

            return Result;
        }

        public bool DeletLeaveReason(int Id, TokenModel token)
        {
            LeaveReason leaveReasonEntity = _leaveReasonReapository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            leaveReasonEntity.IsDeleted = true;
            leaveReasonEntity.IsActive = false;
            leaveReasonEntity.DeletedBy = 2;// token.UserID;
            leaveReasonEntity.DeletedDate = DateTime.UtcNow;
            _leaveReasonReapository.SaveChanges();

            return true;
        }
    }
}
