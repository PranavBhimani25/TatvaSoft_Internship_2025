using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Services.Services.Interface;

namespace User_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController(IMissionService missionService) : Controller
    {
        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            return new ResponseResult() { Data = missionService.GetMissionList(), Message = "", Result = ResponseStatus.Success };
        }

        [HttpPost]
        [Route("AddMission")]
        public ActionResult AddMission(AddMissionRequestModel model)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                var data = missionService.AddMission(model);
                result.Data = data;
                result.Message = "Success";
                result.Result = ResponseStatus.Success;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("DeleteMission")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            ResponseResult resp = new ResponseResult();
            try
            {
                var result = missionService.DeleteAsync(id);
                resp.Data = result;
                resp.Message = "Success";
                resp.Result = ResponseStatus.Success;
                return Ok(resp);
            }
            catch(Exception ex) 
            {
                resp.Data = null;
                resp.Message = ex.Message;
                resp.Result = ResponseStatus.Error;
                return BadRequest(resp);
            }

            
        }
    }
}
