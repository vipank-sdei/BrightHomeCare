using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Entity.Agency
{
    public class MasterDocumentTypes : BaseEntity
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        public string Type { get; set; }        

        public int? DisplayOrder { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int? OrganizationID { get; set; }

        public Organization Organization { get; set; }
    }
}
