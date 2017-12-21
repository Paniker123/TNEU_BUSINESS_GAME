using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO.AccountDTO;
using Common.DTO.Communication;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController:Controller
    {

        private IUserService _userService;
    

        public AdminController(IUserService userService)
        {
            _userService = userService;
           
        }

        [HttpGet("GetUserInfo/{userId}")]
        public async Task<IActionResult> GetUserInfo([FromRoute]string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _userService.GetUserInfo(userId);
                if (userInfo.Error != null)
                {
                    return StatusCode(userInfo.Error.ErrorCode, userInfo.Error.ErrorDescriprion);
                }

                return Ok(userInfo.Data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(e.Message));
            }
        }
      
       
        [HttpGet("GetAllUsersInfo")]
        public async Task<IActionResult> GetAllUsersInfo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _userService.GetAllUsers();
                if (userInfo.Error != null)
                {
                    return StatusCode(userInfo.Error.ErrorCode, userInfo.Error.ErrorDescriprion);
                }

                return Ok(userInfo.Data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(e.Message));
            }
        }
       
     

    }
}
