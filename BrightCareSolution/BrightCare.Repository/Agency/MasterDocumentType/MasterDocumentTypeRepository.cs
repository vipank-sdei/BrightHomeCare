using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.MasterDocumentType;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.MasterDocumentType
{
    public class MasterDocumentTypeRepository: RepositoryBase<MasterDocumentTypes>, IMasterDocumentTypeRepository
    {
        private HCOrganizationContext _context;

        public MasterDocumentTypeRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
}
