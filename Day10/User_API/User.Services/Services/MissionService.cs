using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Repositories.Repositories;
using User.Repositories.Repositories.Interface;
using User.Services.Services.Interface;

namespace User.Services.Services
{
    public class MissionService(IMissionRepository missionRepository, IMissionSkillRepository missionSkillRepository) : IMissionService
    {
        private readonly IMissionRepository _missionRepository = missionRepository;
        private readonly IMissionSkillRepository _missionSkillRepository = missionSkillRepository;

        public List<Missions> GetMissionList()
        {
            return missionRepository.GetMissionList();
        }

        public Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return _missionRepository.GetMissionById(id);
        }

        public Task<string> AddMission(AddMissionRequestModel model)
        {
            return missionRepository.AddMission(model);
        }

        // int userId
        public async Task<IList<MissionDetailResponseModel>> ClientSideMissionList()
        {
            var missions = await _missionRepository.ClientSideMissionList();

            return missions.Select(m => new MissionDetailResponseModel()
            {
                Id = m.Id,
                EndDate = m.EndDate,
                StartDate = m.StartDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionTitle = m.MissionTitle,
                TotalSheets = m.TotalSheets,
                RegistrationDeadLine = m.RegistrationDeadLine,
                CityId = m.CityId,
                CityName = m.City.CityName,
                CountryId = m.CountryId,
                CountryName = m.Country.CountryName,
                MissionSkillId = m.MissionSkillId,
                MissionSkillName = _missionSkillRepository.GetMissionSkills(m.MissionSkillId),
                MissionThemeId = m.MissionThemeId,
                MissionThemeName = m.MissionTheme.ThemeName
            }).ToList();

        }
        public string DeleteAsync(int id)
        {
            return _missionRepository.DeleteMission(id);
        }

    }
}
