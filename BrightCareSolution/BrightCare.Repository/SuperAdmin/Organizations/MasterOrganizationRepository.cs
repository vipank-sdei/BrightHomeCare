using BrightCare.Entity.SuperAdmin;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.SuperAdmin.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.SuperAdmin.Organizations
{

    public class MasterOrganizationRepository : MasterRepositoryBase<MasterOrganization>, IMasterOrganizationRepository
    {
        private HCMasterContext masterContext;
        private HCOrganizationContext _context;
        public MasterOrganizationRepository(HCMasterContext masterContext, HCOrganizationContext context) : base(masterContext)
        {
            this.masterContext = masterContext;
            _context = context;//to check what is the need of org context  here???
        }

        //public List<MasterOrganizationModel> GetMasterOrganizations(string businessName, string orgName, string country, string sortOrder, string sortColumn, int page, int pageSize)
        //{
        //    SqlParameter[] parameters = { new SqlParameter("@BusinessName", businessName),
        //                                  new SqlParameter("@organizationName",orgName),
        //                                  new SqlParameter("@Country",country),
        //                                  new SqlParameter("@PageNumber",page),
        //                                  new SqlParameter("@PageSize",pageSize),
        //                                  new SqlParameter("@SortColumn",sortColumn),
        //                                  new SqlParameter("@SortOrder",sortOrder),
        //    };
        //    return _masterContext.ExecStoredProcedureListWithOutput<MasterOrganizationModel>("Org_GetMasterOrganization", parameters.Length, parameters).ToList();
        //}
        //public OrganizationDetailModel GetOrganizationDetailsById(TokenModel token)
        //{
        //    SqlParameter[] parameters = { new SqlParameter("@OrganizatonID",token.OrganizationID ),
        //    };
        //    return _context.ExecStoredProcedureListWithOutput<OrganizationDetailModel>(SQLObjects.ORG_GetOrganizationData, parameters.Length, parameters).FirstOrDefault();
        //}
    }
}
