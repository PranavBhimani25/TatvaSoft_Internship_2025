using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Context;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Repositories.Repositories.Interface;

namespace User.Repositories.Repositories
{
    public class MissionRepository(UserDbContext dbContext) : IMissionRepository
    {
        private readonly UserDbContext _dbContext = dbContext;

        public List<Missions> GetMissionList()
        {
            return dbContext.Missions.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<string> AddMission(AddMissionRequestModel model)
        {
            var isExist = dbContext.Missions.Where(x =>
                            x.MissionTitle == model.MissionTitle
                            && x.StartDate == model.StartDate
                            && x.EndDate == model.EndDate
                            && x.CityId == model.CityId
                            && !x.IsDeleted
                        ).FirstOrDefault();

            if (isExist != null) throw new Exception("Mission already exist!");

            Missions missions = new Missions()
            {
                MissionTitle = model.MissionTitle,
                MissionDescription = model.MissionDescription,
                MissionImages = model.MissionImages,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CountryId = model.CountryId,
                CityId = model.CityId,
                TotalSheets = model.TotalSheets,
                MissionThemeId = model.MissionThemeId,
                MissionSkillId = model.MissionSkillId,
                MissionOrganisationName = "",
                MissionOrganisationDetail = "",
                MissionType = "",
                MissionDocuments = "",
                MissionAvailability = "",
                MissionVideoUrl = "",


                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };
            await dbContext.Missions.AddAsync(missions);
            dbContext.SaveChanges();

            return "Added!";
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }

        // int userId
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public string DeleteMission(int id)
        {
            _dbContext.Missions
                .Where(ms => ms.Id == id)
                .ExecuteUpdate(ms => ms.SetProperty(property => property.IsDeleted, true));

            return "Delete Mission Successfully..";
        }
    }
}
