using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.LeaveReasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.LeaveReasons
{
    public interface ILeaveReasonService
    {
        JsonModel GetLeaveReason(TokenModel token);
        JsonModel AddUpdateLeaveReason(LeaveReasonDTO leaveReasonDTO, TokenModel token);
        bool DeletLeaveReason(int Id, TokenModel token);
    }
}
