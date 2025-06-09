using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.ViewModels;
using User.Repositories.Repositories.Interface;
using User.Services.Services.Interface;

namespace User.Services.Services
{
    public class MissionSkillService(IMissionSkillRepository missionSkillRepository) : IMissionSkillService
    {
        private readonly IMissionSkillRepository _missionSkillRepository = missionSkillRepository;

        public List<MissionSkillResponseModel> GetMissionSkillList()
        {
            return _missionSkillRepository.GetMissionSkillList();
        }

        public MissionSkillResponseModel GetMissionSkillById(int id)
        {
            return _missionSkillRepository.GetMissionSkillById(id);
        }

        public string AddMissionSkill(AddMissionSkillRequestModel model)
        {
            return _missionSkillRepository.AddMissionSkill(model);
        }

        public string UpdateMissionSkill(UpdateMissionSkillRequestModel model)
        {
            return _missionSkillRepository.UpdateMissionSkill(model);
        }

        public string DeleteMissionSkill(int id)
        {
            return _missionSkillRepository.DeleteMissionSkill(id);
        }
    }
}
