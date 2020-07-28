using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency.UserRole
{
    public class UserRoleDTO
    {
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserType { get; set; }
        public int? IOrganizationId { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
                    
        public decimal? TotalRecords { get; set; }
    }
}
