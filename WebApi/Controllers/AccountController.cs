



using System;

using System.Threading.Tasks;
using Common.DTO.AccountDTO;
using Common.DTO.Communication;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
   
    [Route("api/[controller]")]
    //  [RoutePrefix("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _accountService;

         //private readonly ILogger<AccountController> _logger;

       


        public AccountController(IUserService accountService
            // ,ILogger<AccountController> logger
            )
        {
            _accountService = accountService;
            // _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccount createAccount)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }
            try
            {
                var response = await _accountService.CreateAccount(createAccount);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode,response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                //_logger.LogError(0,ex,"Failed to register new User");
                return StatusCode(500,new Error(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody]LogInAccount logInAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var identity = await _accountService.LogIn(logInAccount);
                if (identity.Error != null)
                {
                    return StatusCode(identity.Error.ErrorCode, identity.Error.ErrorDescriprion);
                }

                var response = await _accountService.GetToken(identity.Data);
                if (response.Error != null)
                {
                    return StatusCode(identity.Error.ErrorCode, identity.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(e.Message));
            }
        }




       
        [HttpGet("GetCurrentUserInfo/{userId}")]
        public async Task<IActionResult> GetCurrentUserInfo([FromRoute]string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _accountService.GetCurrentUserInfo(userId);
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