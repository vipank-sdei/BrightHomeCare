using BrightCare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightCare.Repository.Interface.Agency.StaffLeave
{
    public interface IStaffLeaveRepository : IRepositoryBase<Entity.Agency.StaffLeaves>
    {

        IQueryable<T> GetStaffLeaveList<T>(TokenModel token) where T : class, new();

    }
}
