using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency.DiagnosisCodes
{
    public class DiagnosisCodesDTO
    {
        public int? Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }        
        public string ICDCode { get; set; }
        public string Description { get; set; }        
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? IOrganizationId { get; set; }
        public decimal? TotalRecords { get; set; }
    }
}
