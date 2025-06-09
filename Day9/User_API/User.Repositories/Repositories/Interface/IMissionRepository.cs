using User.Entities.Entities;
using User.Entities.ViewModels;

namespace User.Repositories.Repositories.Interface
{
    public interface IMissionRepository
    {
        List<Missions> GetMissionList();
        Task<string> AddMission(AddMissionRequestModel model);
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<IList<Missions>> ClientSideMissionList();
        string DeleteMission(int id);

    }
}
