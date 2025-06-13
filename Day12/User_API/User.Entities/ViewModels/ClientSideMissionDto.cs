using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entities.ViewModels
{
    public class ClientSideMissionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string MissionThemeTitle { get; set; }
        public DateTime CreatedDate { get; set; }

        public string MissionStatus { get; set; }
        public string MissionApproveStatus { get; set; }
        public string MissionApplyStatus { get; set; }
    }

}
