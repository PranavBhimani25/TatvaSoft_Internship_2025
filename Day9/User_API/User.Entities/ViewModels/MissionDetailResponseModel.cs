﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entities.ViewModels
{
    public class MissionDetailResponseModel
    {
        public int Id { get; set; }

        public string MissionTitle { get; set; }

        public string MissionDescription { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? TotalSheets { get; set; }
        public DateTime? RegistrationDeadLine { get; set; }

        public int MissionThemeId { get; set; }
        public string MissionThemeName { get; set; }

        public string MissionSkillId { get; set; }
        public string MissionSkillName { get; set; }

        public string MissionImages { get; set; }
    }
}
