using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.Staffs
{
   public interface IStaffExperienceService
    {
    JsonModel CreateUpdateStaffExperience(StaffExperienceRequestDTO staffExperienceRequestDTO, TokenModel token);

    JsonModel GetStaffExperienceByUserID(int UserId, TokenModel token);

    JsonModel GetStaffExperienceByID(int id, TokenModel token);

    JsonModel DeleteStaffExperience(int id, TokenModel token);
    }
}
