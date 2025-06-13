using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Entities;
using User.Entities.ViewModels;

namespace User.Repositories.Repositories.Interface
{
    public interface IMissionSkillRepository
    {
        List<MissionSkillResponseModel> GetMissionSkillList();

        MissionSkillResponseModel GetMissionSkillById(int id);

        string AddMissionSkill(AddMissionSkillRequestModel model);

        string UpdateMissionSkill(UpdateMissionSkillRequestModel model);

        string DeleteMissionSkill(int id);

        string GetMissionSkills(string skillIds);

    }

}
