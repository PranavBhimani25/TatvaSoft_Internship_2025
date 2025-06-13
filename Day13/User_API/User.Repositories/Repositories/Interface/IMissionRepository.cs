using User.Entities.Entities;
using User.Entities.ViewModels;

namespace User.Repositories.Repositories.Interface
{
    public interface IMissionRepository
    {
        List<Missions> GetMissionList();
        Task<string> AddOrUpdateMissionAsync(AddMissionRequestModel model);
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<IList<Missions>> ClientSideMissionList();
        string DeleteMission(int id);


        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);

        List<MissionApplication> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);

    }
}
