using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.Staff;
using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Organizations;
using BrightCare.Common;
using System.Linq;
using Microsoft.Data.SqlClient;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Repository.Agency.Staff
{
   public class StaffRepository : RepositoryBase<Staffs>, IStaffRepository
    {
        private HCOrganizationContext context;
        public StaffRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<T> GetStaff<T>(TokenModel tokenModel) where T : class, new()
        {
            SqlParameter[] parameters = { new SqlParameter("@OrganizationId",tokenModel.OrganizationID),
                                         
            };
            return context.ExecStoredProcedureListWithOutput<T>(SQLObjects.STF_GetStaffUsers.ToString(), parameters.Length, parameters).AsQueryable();
        }
    }
}
