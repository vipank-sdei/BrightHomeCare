using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightCare.Common;
using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.StaffLeave;
using Microsoft.Data.SqlClient;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Repository.Agency.StaffLeave
{
  public  class StaffLeaveRepository : RepositoryBase<Entity.Agency.StaffLeaves>, IStaffLeaveRepository
    {

        private HCOrganizationContext context;
        public StaffLeaveRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<T> GetStaffLeaveList<T>(TokenModel token) where T : class, new()
        {
            SqlParameter[] parameters = {
                new SqlParameter("@OrganizationID", token.OrganizationID),
            };
            return context.ExecStoredProcedureListWithOutput<T>(SQLObjects.STF_GetStaffLeaveList.ToString(), parameters.Length, parameters).AsQueryable();
        }
    }
}
