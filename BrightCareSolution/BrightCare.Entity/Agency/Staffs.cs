using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightCare.Entity.Agency
{
    public class Staffs : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("StaffID")]
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [ForeignKey("Organization")]
        public int OrganizationID { get; set; }
        public string PhotoPath { get; set; }
        [NotMapped]
        public string PhotoBase64 { get; set; }
        public string PhotoThumbnailPath { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        [Required]
        public DateTime DOJ { get; set; }

        [StringLength(50)]
        public string NPINumber { get; set; }

        public string TaxId { get; set; }
        public int RoleID { get; set; }

        [ForeignKey("MasterState")]
        public int? StateID { get; set; }
        [ForeignKey("MasterCountry")]
        public int? CountryID { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(20)]
        public string Zip { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        public int? MaritalStatus { get; set; }

        public bool IsRenderingProvider { get; set; }

        [StringLength(15)]
        public string CAQHID { get; set; }
        public string Language { get; set; }

        public int? EmployeeTypeID { get; set; }

        public int? PayrollGroupID { get; set; }

        public DateTime? TerminationDate { get; set; }
        [StringLength(20)]
        public DateTime? SSN { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public int? DegreeID { get; set; }

        public string EmployeeID { get; set; }

        [StringLength(20)]
        public string ApartmentNumber { get; set; }
        public decimal PayRate { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string AboutMe { get; set; }
      
    }
}
