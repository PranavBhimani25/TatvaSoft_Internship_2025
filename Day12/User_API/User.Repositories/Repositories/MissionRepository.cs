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

        //public async Task<string> AddMission(AddMissionRequestModel model)
        //{
        //var isExist = dbContext.Missions.Where(x =>
        //                x.MissionTitle == model.MissionTitle
        //                && x.StartDate == model.StartDate
        //                && x.EndDate == model.EndDate
        //                && x.CityId == model.CityId
        //                && !x.IsDeleted
        //            ).FirstOrDefault();

        //if (isExist != null) throw new Exception("Mission already exist!");

        //Missions missions = new Missions()
        //{
        //    MissionTitle = model.MissionTitle,
        //    MissionDescription = model.MissionDescription,
        //    MissionImages = model.MissionImages,
        //    StartDate = model.StartDate,
        //    EndDate = model.EndDate,
        //    CountryId = model.CountryId,
        //    CityId = model.CityId,
        //    TotalSheets = model.TotalSheets,
        //    MissionThemeId = model.MissionThemeId,
        //    MissionSkillId = model.MissionSkillId,
        //    MissionOrganisationName = "",
        //    MissionOrganisationDetail = "",
        //    MissionType = "",
        //    MissionDocuments = "",
        //    MissionAvailability = "",
        //    MissionVideoUrl = "",


        //    IsDeleted = false,
        //    CreatedDate = DateTime.Now,
        //};
        //await dbContext.Missions.AddAsync(missions);
        //dbContext.SaveChanges();

        //return "Added!";
        //}


        public async Task<string> AddOrUpdateMissionAsync(AddMissionRequestModel model)
        {
            try
            {
                Missions mission;

                if (model.Id == null || model.Id == 0)
                {
                    mission = new Missions
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
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    await dbContext.Missions.AddAsync(mission);
                }
                else
                {
                    mission = await dbContext.Missions.FindAsync(model.Id);
                    if (mission == null) throw new Exception("Mission not found!");

                    mission.MissionTitle = model.MissionTitle;
                    mission.MissionDescription = model.MissionDescription;
                    mission.MissionImages = model.MissionImages;
                    mission.StartDate = model.StartDate;
                    mission.EndDate = model.EndDate;
                    mission.CountryId = model.CountryId;
                    mission.CityId = model.CityId;
                    mission.TotalSheets = model.TotalSheets;
                    mission.MissionThemeId = model.MissionThemeId;
                    mission.MissionSkillId = model.MissionSkillId;
                    mission.ModifiedDate = DateTime.Now;
                }

                await dbContext.SaveChangesAsync();
                return model.Id == 0 || model.Id == null ? "Mission added successfully." : "Mission updated successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception($"DB Error: {(ex)}");
            }
        }

        private string GetInnerExceptionMessage(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            return ex.Message;
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



        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            try
            {
                var mission = _dbContext.Missions.Where(x => x.Id == model.MissionId).FirstOrDefault();

                if (mission == null) throw new Exception("Mission not found");

                var application = _dbContext.MissionApplications.Where(x => x.MissionId == model.MissionId && x.UserId == model.UserId).FirstOrDefault();

                if (application != null) throw new Exception("Already applied!");

                MissionApplication app = new MissionApplication()
                {
                    UserId = model.UserId,
                    MissionId = model.MissionId,
                    AppliedDate = model.AppliedDate,
                    Seats = model.Sheet,
                    Status = model.Status,

                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };

                mission.TotalSheets -= model.Sheet;

                await _dbContext.MissionApplications.AddAsync(app);
                _dbContext.Missions.Update(mission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MissionApplication> GetMissionApplicationList()
        {
            return _dbContext.MissionApplications.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
