using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.Staff
{
   public class StaffExperienceRepository : RepositoryBase<StaffExperience>, IStaffExperienceRepository
    {
        private HCOrganizationContext context;
        public StaffExperienceRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
