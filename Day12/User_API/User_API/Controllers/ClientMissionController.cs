using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Services.Services.Interface;

namespace User_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientMissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;

        [HttpGet]
        [Route("ClientSideMissionList/{userId}")]
        public async Task<IActionResult> ClientSideMissionList(int userId)
        {
            try
            {
                var missions = await _missionService.ClientSideMissionList(userId);
                return Ok(new ResponseResult() { Data = missions, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Message = "Error in fetching missions for user.", Result = ResponseStatus.Error });

            }
        }

        [HttpPost]
        [Route("ApplyMission")]
        public async Task<IActionResult> ApplyMission(AddMissionApplicationRequestModel model)
        {
            try
            {
                var ret = await _missionService.ApplyMission(model);
                return Ok(new ResponseResult() { Data = ret, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }

        [HttpGet]
        [Route("GetMissionApplicationList")]
        public IActionResult GetMissionApplicationList()
        {
            try
            {
                var ret = _missionService.GetMissionApplicationList();
                return Ok(new ResponseResult() { Data = ret, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }

        [HttpPut]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(UpdateMissionApplicationModel model)
        {
            try
            {
                var ret = await _missionService.MissionApplicationApprove(model);
                return Ok(new ResponseResult() { Data = ret, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}
