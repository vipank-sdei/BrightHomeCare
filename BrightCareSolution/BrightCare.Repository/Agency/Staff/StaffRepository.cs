using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.Staff;
using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Organizations;


namespace BrightCare.Repository.Agency.Staff
{
   public class StaffRepository : RepositoryBase<Staffs>, IStaffRepository
    {
        private HCOrganizationContext context;
        public StaffRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
