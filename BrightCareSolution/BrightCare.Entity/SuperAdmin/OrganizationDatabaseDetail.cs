using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Entity.SuperAdmin
{
    public class OrganizationDatabaseDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServerName { get; set; }

        [MaxLength(100)]
        public string DatabaseName { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }
        public bool IsCentralised { get; set; }
      
    }
}
