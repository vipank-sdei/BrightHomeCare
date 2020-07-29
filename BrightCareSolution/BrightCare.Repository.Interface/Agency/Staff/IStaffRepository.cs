using BrightCare.Common;
using BrightCare.Entity.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightCare.Repository.Interface.Agency.Staff
{
   public interface IStaffRepository : IRepositoryBase<Staffs>
    {
        IQueryable<T> GetStaff<T>(TokenModel token) where T : class, new();

    }
}
