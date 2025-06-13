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
        Task<string> AddOrUpdateMissionAsync(AddMissionRequestModel model);
        Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId);
        string DeleteAsync(int id);


        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);
        List<MissionApplication> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
    }
}
