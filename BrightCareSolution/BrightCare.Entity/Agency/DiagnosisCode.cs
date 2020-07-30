using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Entity.Agency
{
    public class DiagnosisCode : BaseEntity
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string ICDCode { get; set; }        

        public string Description { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
