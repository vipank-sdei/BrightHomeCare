using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.UserRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.UserRole
{
    public class UserRoleRepository : RepositoryBase<UserRoles>, IUserRoleRepository
    {
        private HCOrganizationContext _context;
        public UserRoleRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
   
}
