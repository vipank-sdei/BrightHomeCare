using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.LeaveReasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.LeaveReasons
{
    public class LeaveReasonRepository: RepositoryBase<LeaveReason>, ILeaveReasonReapository
    {
        private HCOrganizationContext _context;
        public LeaveReasonRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
}
