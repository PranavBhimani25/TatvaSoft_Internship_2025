using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entities.ViewModels
{
    public class AddUserDetailsRequestModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmployeeId { get; set; }

        public string Manager { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        public string MyProfile { get; set; }

        public string WhyIVolunteer { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        [Required(ErrorMessage = "The Availability field is required.")]
        public string Availability { get; set; }

        [Required(ErrorMessage = "The LinkedInUrl field is required.")]
        public string LinkedInUrl { get; set; }

        public string MySkills { get; set; }

        public string UserImage { get; set; }

        public bool Status { get; set; }
    }
}
