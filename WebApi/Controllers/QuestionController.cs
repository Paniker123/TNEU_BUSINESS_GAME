

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.QuestionDTO;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class QuestionController:Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody]QuestionDTO question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.CreateQuestion(question);
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

        [HttpPut("ChangeQuestion")]
        public async Task<IActionResult> ChangeQuestion([FromBody]QuestionInfo question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.ChangeQusetion(question);
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
        [HttpGet("GetQuestionById/{questionId}")]
        public async Task<IActionResult> GetQuestionById([FromRoute]int questionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.ShowQuestion(questionId);
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

        [HttpGet("GetQuestions")]
        public async Task<IActionResult> GetQuestions()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.ShowAllQuestions();
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
        [HttpDelete("DeleteQuestionById")]
        public async Task<IActionResult> DeleteQuestionById([FromRoute]int questionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.DeleteQuestion(questionId);
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


        [HttpPost("AddQuestionsToTheTest/{testId}")]
        public async Task<IActionResult> AddQuestionsToTheTest([FromRoute]int testId, [FromBody]ICollection<int> questionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.AddQuesttionsToTheTest(testId, questionId);
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
        [HttpPost("AddQuestionToTheTest")]
        public async Task<IActionResult> AddQuestionToTheTest([FromBody]AddQuestionToTheTest questionAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.AddQuesttionToTheTest(questionAdd.TestId, questionAdd.QuestionId);
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




        [HttpDelete("DeleteQuestionInTheTest/{testId}/{questionId}")]
        public async Task<IActionResult> DeleteQuestionInTheTest([FromRoute]int testId, [FromRoute]int questionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.DeleteQuestionByTestId(testId, questionId);
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

        [HttpGet("GetAllQuestionsInTheTest/{testId}")]
        public async Task<IActionResult> GetAllQuestionsInTheTest([FromRoute]int testId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _questionService.ShowAllQuestionsByTestId(testId);
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
