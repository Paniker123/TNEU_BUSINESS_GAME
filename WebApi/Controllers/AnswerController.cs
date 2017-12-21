using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO.AnswerDTO;
using Common.DTO.Communication;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AnswerController:Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost("CreateAnswer")]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswer createAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.CreateAnswer(createAnswer);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }


        [HttpPut("ChangeAnswer")]
        public async Task<IActionResult> ChangeAnswer([FromBody] ChangeAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.ChangeAnswer(answer);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }

        [HttpGet("GetAnswerById/{answerId}")]
        public async Task<IActionResult> GetAnswerById([FromRoute]int answerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.ShowAnswerById(answerId);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }

        [HttpGet("GetAllAnswer")]
        public async Task<IActionResult> GetAllAnswer()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.ShowAllAnswers();
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }

        [HttpDelete("DeleteAnswerById/{answerId}")]
        public async Task<IActionResult> DeleteAnswerById([FromRoute]int answerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.DeleteAnswer(answerId);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }




        [HttpPost("AddAnswerToTheQuestion")]
        public async Task<IActionResult> AddAnswerToTheQuestion([FromBody]AddAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.AddAnswerToTheQuestion(answer.QuestionId, answer.AnswerId);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }


        [HttpPost("AddAnswerToTheQuestion/{questionId}")]
        public async Task<IActionResult> AddAnswerToTheQuestion([FromRoute]int questionId, [FromBody]ICollection<CreateAnswer> answers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.AddAnswerToTheQuestion(questionId, answers);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }


        [HttpDelete("DeleteAnswerInTheQuestion/{questionId}/{answerId}")]
        public async Task<IActionResult> DeleteAnswerInTheQuestion([FromRoute]int questionId, [FromRoute]int answerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _answerService.DeleteAnswerInTheQuestion(questionId, answerId);
                if (response.Error != null)
                {
                    return StatusCode(response.Error.ErrorCode, response.Error.ErrorDescriprion);
                }
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error(ex.Message));
            }
        }
    }
}
