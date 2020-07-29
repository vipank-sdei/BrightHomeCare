using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency
{
  public class StaffsDTO
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        
        public string Email { get; set; }
      
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
       
        public string LastName { get; set; }
       
        public int OrganizationID { get; set; }
        //public string PhotoPath { get; set; }
       
        //public string PhotoBase64 { get; set; }
        //public string PhotoThumbnailPath { get; set; }

        public int UserID { get; set; }
        public DateTime DOJ { get; set; }

        public string NPINumber { get; set; }

        public string TaxId { get; set; }
        public int RoleID { get; set; }

        public int? StateID { get; set; }
        public int? CountryID { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int? MaritalStatus { get; set; }

        public bool IsRenderingProvider { get; set; }

        public string CAQHID { get; set; }
        public string Language { get; set; }

        public int? EmployeeTypeID { get; set; }

        public int? PayrollGroupID { get; set; }

        public DateTime? TerminationDate { get; set; }
        public DateTime? SSN { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public int? DegreeID { get; set; }

        public string EmployeeID { get; set; }

        public string ApartmentNumber { get; set; }
        public decimal PayRate { get; set; }

        public string AboutMe { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }
       
    }
}
