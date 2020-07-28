using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightCare.Entity.Agency
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserID")]
        public int Id { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsBlock { get; set; }
        public DateTime? BlockDateTime { get; set; }

        public bool IsOnline { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int OrganizationID { get; set; }

        public Nullable<DateTime> PasswordResetDate { get; set; }

        public virtual Organization Organization { get; set; }

        public int? ExpirationPeriodInDays { get; set; }
        public bool? IsSecurityQuestionMandatory { get; set; }
    }
}
