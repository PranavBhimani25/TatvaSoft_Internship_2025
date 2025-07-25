﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;
using User.Services.Services.Interface;

namespace User_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientMissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;

        [HttpGet]
        [Route("ClientSideMissionList")]
        public async Task<IActionResult> ClientSideMissionList()
        {
            try
            {
                var missions = await _missionService.ClientSideMissionList();
                return Ok(new ResponseResult() { Data = missions, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Message = "Error in fetching missions for user.", Result = ResponseStatus.Error });

            }
        }
    }
}
