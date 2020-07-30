using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.DiagnosisCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Agency.DiagnosisCodes
{
    public class DiagnosisCodesRepository: RepositoryBase<DiagnosisCode>, IDiagnosisCodesRepository
    {
        private HCOrganizationContext _context;
        public DiagnosisCodesRepository(HCOrganizationContext context) : base(context)
        {
            this._context = context;
        }
    }
}
