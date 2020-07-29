using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.LeaveTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.LeaveTypes
{
    public class LeaveTypeRepository: RepositoryBase<LeaveType>, ILeaveTypeRepository
    {
        private HCOrganizationContext _context;
        public LeaveTypeRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
}
