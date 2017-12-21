using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.TestDTO;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class TestController:Controller
    {
       
        private ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost("CreateTest")]
        public async Task<IActionResult> CreateTest([FromBody]string NameTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _testService.CreateTest(NameTest);
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
        [HttpPut("ChangeTest")]
        public async Task<IActionResult> ChangeTest([FromBody]ChangeTest test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _testService.ChangeTest(test);
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
        [HttpGet("GetAllTests")]
        public async Task<IActionResult> GetAllTests()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _testService.ShowAllTests();
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
        [HttpGet("GetTestById/{testId}")]
        public async Task<IActionResult> GetTestById([FromRoute]int testId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _testService.ShowTestById(testId);
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
        [HttpDelete("DeleteTest/{testId}")]
        public async Task<IActionResult> DeleteTest([FromRoute]int testId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _testService.DeleteTest(testId);
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
        [HttpPut("AddTestToTheGame/{testId}/{gameId}")]
        public async Task<IActionResult> AddTestToTheGame([FromRoute]int testId,[FromRoute] int gameId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userInfo = await _testService.AddTestToTheGame(gameId, testId);
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
