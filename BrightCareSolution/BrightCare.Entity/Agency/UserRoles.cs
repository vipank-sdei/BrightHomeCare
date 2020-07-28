using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Entity.Agency
{
    public class UserRoles
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("RoleID")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]

        public string RoleName { get; set; }
        public string UserType { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int OrganizationID { get; set; }

        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }


        // public virtual User Users { get; set; }
        public Organization Organization { get; set; }
    }
}
