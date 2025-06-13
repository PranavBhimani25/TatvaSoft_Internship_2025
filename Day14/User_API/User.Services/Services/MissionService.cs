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

        public async Task<string> AddOrUpdateMissionAsync(AddMissionRequestModel model)
        {
            return await missionRepository.AddOrUpdateMissionAsync(model);
        }

        // int userId
        public async Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId)
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
                MissionThemeName = m.MissionTheme.ThemeName,

                MissionApplyStatus = m.MissionApplications.Any(m => m.UserId == userId) ? "Applied" : "Apply",
                MissionApproveStatus = m.MissionApplications.Any(m => m.UserId == userId && m.Status == true) ? "Approved" : "Applied",
                MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available"

            }).ToList();

        }

        
        public string DeleteAsync(int id)
        {
            return _missionRepository.DeleteMission(id);
        }


        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            return await _missionRepository.ApplyMission(model);
        }

        public List<MissionApplication> GetMissionApplicationList()
        {
            return _missionRepository.GetMissionApplicationList();
        }

        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            return await _missionRepository.MissionApplicationApprove(missionApplication);
        }

    }
}
