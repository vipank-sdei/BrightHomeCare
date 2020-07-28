using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.Organizations
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        private HCOrganizationContext context;
        public OrganizationRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
