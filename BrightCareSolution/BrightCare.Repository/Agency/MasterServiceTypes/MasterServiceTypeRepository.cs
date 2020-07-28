using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.MasterServiceTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.MasterServiceTypes
{
    public class MasterServiceTypeRepository : RepositoryBase<MasterServiceType>, IMasterServiceTypeRepository
    {
        private HCOrganizationContext _context;
        public MasterServiceTypeRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }

        public MasterServiceType AddMasterServiceType(MasterServiceType masterServiceType)
        {
            _context.MasterServiceType.Add(masterServiceType);
            _context.SaveChanges();


            return masterServiceType;
        }
    }
}
