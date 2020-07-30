using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.MasterAllergy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.MasterAllergy
{
    public class MasterAllergyRepository : RepositoryBase<MasterAllergies>, IMasterAllergyRepository
    {
        private HCOrganizationContext _context;
        public MasterAllergyRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
}
