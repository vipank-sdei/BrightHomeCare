using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.StaffLeave;
using BrightCare.Repository.Interface.Agency.StaffLeave;
using BrightCare.Service.Interface.Agency.StaffLeave;
using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Entity.Agency;
using HC.Common;
using static BrightCare.Common.Enums.CommonEnum;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BrightCare.Service.Agency.StaffLeave
{
   public class StaffLeaveService : IStaffLeaveService
    {
        private readonly IStaffLeaveRepository iStaffLeaveRepository;
        private JsonModel response;
        private readonly IMapper _mapper;

        public StaffLeaveService(IStaffLeaveRepository iStaffLeaveRepository, IMapper mapper)
        {
            this.iStaffLeaveRepository = iStaffLeaveRepository;
            this._mapper = mapper;

        }
        public JsonModel CreateUpdateStaffLeave(StaffLeaveDTO staffLeaveDTO, TokenModel token)
        {
            token.UserID = 2;
            token.OrganizationID = 1;
            StaffLeaves staffLeave = null;
            if (staffLeaveDTO.Id == 0)
            {
                staffLeave = new StaffLeaves();
                staffLeave = _mapper.Map<StaffLeaves>(staffLeaveDTO);
                staffLeave.CreatedBy = token.UserID;
                staffLeave.IsActive = true;
                staffLeave.CreatedDate = DateTime.UtcNow;
                staffLeave.IsDeleted = false;
                iStaffLeaveRepository.Create(staffLeave);
                iStaffLeaveRepository.SaveChanges();
                if (staffLeave.Id != 0)
                    response = new JsonModel(staffLeave, StatusMessage.StaffLeaveApplied, (int)HttpStatusCodes.OK, string.Empty);
            }
            else
            {
                if (staffLeaveDTO != null)
                {
                    staffLeave = iStaffLeaveRepository.GetFirstOrDefault(a => a.Id == staffLeaveDTO.Id && a.IsDeleted == false && a.IsActive == true);
                    if (staffLeave != null)
                    {
                        staffLeave.Description = staffLeaveDTO.Description;
                        staffLeave.FromDate = staffLeaveDTO.FromDate;
                        staffLeave.LeaveReasonId = staffLeaveDTO.LeaveReasonId;
                        staffLeave.LeaveTypeId = staffLeaveDTO.LeaveTypeId;
                        staffLeave.StaffId = staffLeaveDTO.StaffId;
                        staffLeave.ToDate = staffLeaveDTO.ToDate;
                        staffLeave.UpdatedBy = token.UserID;
                        staffLeave.UpdatedDate = DateTime.UtcNow;
                        iStaffLeaveRepository.Update(staffLeave);
                        iStaffLeaveRepository.SaveChanges();
                        //response
                        response = new JsonModel(null, StatusMessage.StaffLeaveAppliedUpdated, (int)HttpStatusCodes.OK, string.Empty);
                    }
                    else
                        response = new JsonModel(null, StatusMessage.StaffLeaveAppliedDoesNotExist, (int)HttpStatusCodes.BadRequest, string.Empty);
                }
            }
            return response;
        }

        public JsonModel DeleteAppliedLeave(int staffLeaveId, TokenModel token)
        {
            token.UserID = 2;
            StaffLeaves staffLeave = iStaffLeaveRepository.GetFirstOrDefault(x => x.Id == staffLeaveId && x.IsActive == true && x.IsDeleted == false);
            if (!ReferenceEquals(staffLeave, null))
            {
                staffLeave.IsDeleted = true;
                staffLeave.DeletedBy = token.UserID;
                staffLeave.DeletedDate = DateTime.UtcNow;
                iStaffLeaveRepository.Update(staffLeave);
                iStaffLeaveRepository.SaveChanges();
                response = new JsonModel(null, StatusMessage.StaffAppliedLeaveDelete, (int)HttpStatusCodes.OK, string.Empty);

            }
            else
                response = new JsonModel(null, StatusMessage.StaffLeaveAppliedDoesNotExist, (int)HttpStatusCodes.BadRequest, string.Empty);
            return response;
        }

        public JsonModel GetStaffLeave(int? StaffLeaveId, TokenModel token)
        {
            token.OrganizationID = 2;
            if (StaffLeaveId != null)
            {
                StaffLeaves staffLeave = iStaffLeaveRepository.GetFirstOrDefault(a => a.Id == StaffLeaveId && a.IsActive == true && a.IsDeleted == false);
                if (staffLeave != null)
                {
                    StaffLeaveDTO staffLeaveDTO = _mapper.Map<StaffLeaveDTO>(staffLeave);
                    response = new JsonModel(staffLeaveDTO, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK, string.Empty);
                }
                else
                    response = new JsonModel(null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, string.Empty);
            }
            else {

                List<StaffLeaveDTO> staffLeaveModelList = iStaffLeaveRepository.GetStaffLeaveList<StaffLeaveDTO>(token).ToList();
                response = new JsonModel(staffLeaveModelList, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
            }
            return response;
        }
    }
}
