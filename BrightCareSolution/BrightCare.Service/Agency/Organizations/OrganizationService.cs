using BrightCare.Repository.Interface.Agency.Organizations;
using BrightCare.Service.Interface.Agency.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightCare.Service.Agency.Organizations
{
    public class OrganizationService: IOrganizationService
    {
        private readonly IOrganizationRepository iOrganizationRepository;
        public OrganizationService(IOrganizationRepository iOrganizationRepository)
        {
            this.iOrganizationRepository = iOrganizationRepository;
        }
        public void GetOrganizations()
        {
            var list = iOrganizationRepository.GetAll().ToList();
        }
    }
}
