using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.ViewModels;

namespace User.Services.Services.Interface
{
    public interface IMissionThemeService
    {
        Task<List<MissionThemeViewModel>> GetAllMissionTheme();

        Task<MissionThemeViewModel?> GetMissionThemeById(int missionThemeId);

        Task<bool> AddMissionTheme(MissionThemeViewModel model);

        Task<bool> UpdateMissionTheme(MissionThemeViewModel model);

        Task<bool> DeleteMissionTheme(int missionThemeId);
    }
}
