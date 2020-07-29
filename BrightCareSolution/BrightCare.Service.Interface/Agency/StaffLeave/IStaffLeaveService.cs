using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.StaffLeave;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.StaffLeave
{
    public interface IStaffLeaveService
    {
         JsonModel CreateUpdateStaffLeave(StaffLeaveDTO staffLeaveDTO, TokenModel token);

        JsonModel DeleteAppliedLeave(int id, TokenModel token);

        JsonModel GetStaffLeave(int? id, TokenModel token);
    }
}
