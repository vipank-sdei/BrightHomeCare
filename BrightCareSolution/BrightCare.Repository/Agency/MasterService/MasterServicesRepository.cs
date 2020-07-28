using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.MasterService;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Repository.Agency.MasterService
{
    public class MasterServicesRepository : RepositoryBase<MasterServices>, IMasterServicesRepository
    {
        private HCOrganizationContext _context;
        public MasterServicesRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }       
    
             public MasterServices AddMasterService(MasterServices masterService)
        {            
                _context.MasterServices.Add(masterService);
                _context.SaveChanges();

           
            return masterService;
        }
    }
}
