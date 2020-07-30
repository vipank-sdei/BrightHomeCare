using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Entity.Agency
{
   public class UserDocument :  BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserDocumentId")]

        public int Id { get; set; }
        [Required]

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("MasterDocumentTypes")]
        public int? DocumentTypeId { get; set; }
       
        [StringLength(100)]
        public String DocumentName { get; set; }
        [StringLength(50)]
        public String DocumentNumber { get; set; }
        public DateTime? Expiration { get; set; }
        public String OtherDocumentType { get; set; }
        public string UploadPath { get; set; }
        public string Key { get; set; }

        [Obsolete]
        public virtual User User { get; set; }
        public MasterDocumentTypes MasterDocumentTypes { get; set; }


    }
}
