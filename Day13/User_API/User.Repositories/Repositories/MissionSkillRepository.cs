﻿using Microsoft.EntityFrameworkCore;
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
    public class MissionSkillRepository(UserDbContext cIDbContext) : IMissionSkillRepository
    {
        private readonly UserDbContext _cIDbContext = cIDbContext;

        public List<MissionSkillResponseModel> GetMissionSkillList()
        {
            var missionSkill = _cIDbContext.MissionSkills
                .Where(ms => !ms.IsDeleted)
                .Select(ms => new MissionSkillResponseModel()
                {
                    Id = ms.Id,
                    SkillName = ms.SkillName,
                    Status = ms.Status
                })
                .ToList();

            return missionSkill;
        }

        public MissionSkillResponseModel GetMissionSkillById(int id)
        {
            var missionSkillDetail = _cIDbContext.MissionSkills
                .Where(ms => ms.Id == id && !ms.IsDeleted)
                .Select(ms => new MissionSkillResponseModel()
                {
                    Id = ms.Id,
                    SkillName = ms.SkillName,
                    Status = ms.Status
                })
                .FirstOrDefault() ?? throw new Exception("Mission skill not found.");

            return missionSkillDetail;
        }

        public string AddMissionSkill(AddMissionSkillRequestModel model)
        {
            var skillExist = _cIDbContext.MissionSkills.Any(ms => ms.SkillName.ToLower() == model.SkillName.ToLower() && !ms.IsDeleted);

            if (skillExist) throw new Exception("Skill Name Already Exist.");

            var newSkill = new MissionSkill()
            {
                SkillName = model.SkillName,
                Status = model.Status,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = null,
                IsDeleted = false
            };

            _cIDbContext.MissionSkills.Add(newSkill);
            _cIDbContext.SaveChanges();

            return "Save Skill Successfully..";
        }

        public string UpdateMissionSkill(UpdateMissionSkillRequestModel model)
        {
            var skillToUpdate = _cIDbContext.MissionSkills.FirstOrDefault(ms => ms.Id == model.Id && !ms.IsDeleted) ?? throw new Exception("Mission Skill not found.");

            bool skillAlreadyExists = _cIDbContext.MissionSkills
                .Any(ms => ms.Id != model.Id
                    && ms.SkillName.ToLower() == model.SkillName.ToLower()
                    && !ms.IsDeleted);

            if (skillAlreadyExists) throw new Exception("Skill Name Already Exist.");

            skillToUpdate.SkillName = model.SkillName;
            skillToUpdate.Status = model.Status;
            skillToUpdate.ModifiedDate = DateTime.UtcNow;

            _cIDbContext.MissionSkills.Update(skillToUpdate);
            _cIDbContext.SaveChanges();

            return "Update Mission Skill Successfully..";
        }

        public string DeleteMissionSkill(int id)
        {
            _cIDbContext.MissionSkills
                .Where(ms => ms.Id == id)
                .ExecuteUpdate(ms => ms.SetProperty(property => property.IsDeleted, true));

            return "Delete Mission Skill Successfully..";
        }

        public string GetMissionSkills(string skillIds)
        {
            List<string> skillIdList = skillIds.Split(",").ToList();

            return string.Join(",", _cIDbContext.MissionSkills
                .Where(ms => skillIdList.Contains(ms.Id.ToString()))
                .Select(ms => ms.SkillName).ToList());
        }
    }
}
