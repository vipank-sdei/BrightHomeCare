using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency;
using BrightCare.Repository.Interface.Agency.MasterAllergyReaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.MasterAllergyReaction
{
    public class MasterAllergyReactionRepository: RepositoryBase<MasterAllergiesReaction>, IMasterAllergyReactionRepository
    {
        private HCOrganizationContext _context;
        public MasterAllergyReactionRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
}
