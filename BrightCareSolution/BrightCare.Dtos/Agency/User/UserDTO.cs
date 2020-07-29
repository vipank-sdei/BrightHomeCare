using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency
{
   public class UserDTO
    {
        public int Id { get; set; }

        public int? CreatedBy { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BlockDateTime { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool IsOnline { get; set; }

        public int? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int OrganizationID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Nullable<DateTime> PasswordResetDate { get; set; }
        public int? ExpirationPeriodInDays { get; set; }
        public bool? IsSecurityQuestionMandatory { get; set; }
    }
}
