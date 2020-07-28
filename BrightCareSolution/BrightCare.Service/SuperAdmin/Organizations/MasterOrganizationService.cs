using BrightCare.Repository.Interface.SuperAdmin.Organizations;
using BrightCare.Service.Interface.SuperAdmin.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightCare.Service.SuperAdmin.Organizations
{
    public class MasterOrganizationService: IMasterOrganizationService
    {
        private readonly IMasterOrganizationRepository iMasterOrganizationRepository;
        public MasterOrganizationService(IMasterOrganizationRepository iMasterOrganizationRepository)
        {
            this.iMasterOrganizationRepository = iMasterOrganizationRepository;
        }
        public void GetOrganizations()
        {
          var list =   iMasterOrganizationRepository.GetAll().ToList();
        }
    }
}
