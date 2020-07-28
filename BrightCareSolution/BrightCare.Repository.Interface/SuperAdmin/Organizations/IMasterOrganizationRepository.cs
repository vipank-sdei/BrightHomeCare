using BrightCare.Common;
using BrightCare.Entity.SuperAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Repository.Interface.SuperAdmin.Organizations
{
    public interface IMasterOrganizationRepository : IMasterRepositoryBase<MasterOrganization>
    {
        //List<MasterOrganizationModel> GetMasterOrganizations(string businessName, string orgName, string country, string sortOrder, string sortColumn, int page, int pageSize);
        //OrganizationDetailModel GetOrganizationDetailsById(TokenModel token);
    }
}
