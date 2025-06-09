using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Entities;
using User.Entities.ViewModels;

namespace User.Services.Services.Interface
{
    public interface IMissionService
    {
        List<Missions> GetMissionList();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<string> AddMission(AddMissionRequestModel model);
        Task<IList<MissionDetailResponseModel>> ClientSideMissionList();
        string DeleteAsync(int id);
    }
}
