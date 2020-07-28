using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Organizations;
using BrightCare.Repository.Interface.Agency.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.Users
{
   public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        private HCOrganizationContext context;
        public UserRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
