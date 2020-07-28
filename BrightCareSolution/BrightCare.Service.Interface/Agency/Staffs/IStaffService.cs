using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Interface.Agency.Staffs
{
   public interface IStaffService
    {
        JsonModel CreateUpdateStaff(StaffsDTO staffDTO,TokenModel token);

        JsonModel DeleteStaff(int id, TokenModel token);

        JsonModel GetStaff(int? id, TokenModel token);
    }
}
