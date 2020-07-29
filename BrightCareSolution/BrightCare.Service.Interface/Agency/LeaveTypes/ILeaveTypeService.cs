using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.LeaveTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.LeaveTypes
{
    public interface ILeaveTypeService
    {
        JsonModel GetLeaveType(TokenModel token);
        JsonModel AddUpdateLeaveType(LeaveTypeDTO leaveTypeDTO, TokenModel token);
        bool DeletLeaveType(int Id, TokenModel token);
    }
}
