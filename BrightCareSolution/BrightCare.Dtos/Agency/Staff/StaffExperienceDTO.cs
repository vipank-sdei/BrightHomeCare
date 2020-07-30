using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency.Staff
{
   public class StaffExperienceDTO
    {
            public int Id { get; set; }
            public string OrganizationName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string TotalExperience { get; set; }
            public int StaffId { get; set; }
    }

    public class StaffExperienceRequestDTO
    {
        public int staffId { get; set; }
        public List<StaffExperienceDTO> staffExperiences { get; set; }
    }

    public class StaffExperienceRequestDTO1
    {
        public string staffId { get; set; }
       // public List<StaffExperienceDTO> staffExperiences { get; set; }
    }

}
