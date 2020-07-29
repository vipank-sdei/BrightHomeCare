using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightCare.Entity.Agency
{
    public class Patients : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PatientID")]
        public int Id { get; set; }
        public byte[] MRN { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] MiddleName { get; set; }
        public byte[] LastName { get; set; }
        [StringLength(100)]
        public string ClientID { get; set; }

        [Required]
        public int Gender { get; set; }

        public byte[] DOB { get; set; }

        public byte[] SSN { get; set; }

        public byte[] Email { get; set; }

        public bool? OptOut { get; set; }


        public int? MaritalStatus { get; set; }

        [StringLength(100)]
        public string PrimaryProvider { get; set; }


        // [ForeignKey("RenderingProvider")]
        public int? RenderingProviderID { get; set; }

        [StringLength(100)]
        public string EmergencyContactFirstName { get; set; }

        [StringLength(100)]
        public string EmergencyContactLastName { get; set; }

        [StringLength(20)]
        public string EmergencyContactPhone { get; set; }


        [StringLength(100)]
        public string EmergencyContactOthers { get; set; }
        public string PhotoPath { get; set; }

        [NotMapped]
        public string PhotoBase64 { get; set; }

        public string PhotoThumbnailPath { get; set; }

        //public int? Citizenship { get; set; }
        public string Note { get; set; }

        public int LocationID { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey("Users2")]
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [NotMapped]
        public DateTime FromDate
        {
            get
            {
                try
                {
                    return this.CreatedDate;
                }
                catch (Exception)
                {

                    return DateTime.UtcNow;
                }
            }
        }
        [NotMapped]
        public DateTime ToDate
        {
            get
            {
                try
                {
                    return this.CreatedDate;
                }
                catch (Exception)
                {

                    return DateTime.UtcNow;
                }
            }
        }
        [NotMapped]
        public DateTime FromDOB
        {
            get
            {
                try
                {
                    return DateTime.Now;
                    //return this.DOB;
                }
                catch (Exception)
                {

                    return DateTime.UtcNow;
                }
            }
        }
        [NotMapped]
        public DateTime ToDOB
        {
            get
            {
                try
                {
                    return DateTime.Now;
                    //return this.DOB;
                }
                catch (Exception)
                {

                    return DateTime.UtcNow;
                }
            }
        }

       

        [ForeignKey("Users")]
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("Users1")]
        public int? UpdatedBy { get; set; }
        public bool IsVerified { get; set; }

        [NotMapped]
        public string StaffName
        {
            get
            {
                try
                {
                    return Staff != null ? (Staff.FirstName + " " + Staff.LastName) : null;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }



        [NotMapped]
        public string PatientNumber
        {
            get
            {
                try
                {
                    return null;
                    //return PhoneNumbers.FirstOrDefault().PhoneNumber;
                }
                catch (Exception)
                {

                    return null;
                }
            }

        }

        [Required]
        public int OrganizationID { get; set; }
        public bool IsPortalActivate { get; set; }

        public bool IsPortalRequired { get; set; }

        /// <summary>
        /// following tables are used for relationship
        /// </summary>

        public virtual Staffs Staff { get; set; }

        public virtual User Users { get; set; }
        public virtual User Users1 { get; set; }
        public virtual User Users2 { get; set; }
        //public virtual User Users3 { get; set; }

    }
}
