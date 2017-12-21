using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.QuestionDTO;
using Common.Entity;
using Common.Enum;
using Common.Interfaces.DTO;
using Common.Interfaces.Entity;
using Common.Interfaces.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Services.QuestionService
{
    public class QuestionService:IQuestionService
    {

        private readonly MSContext _context;
     

        public QuestionService(MSContext context)
        {
            _context = context;
        }

        public async Task<Response<IQuestion>> CreateQuestion(QuestionDTO createQuestion)
        {
            var response = new Response<IQuestion>();

            try
            {
                if (createQuestion.Name==null)
                {
                    response.Error=new Error(400,"Not valid model");
                    return response;
                }
                var question=new QuestionEntity()
                {
                    Name=createQuestion.Name,
                    Description = createQuestion.Description,
                    AnswerForQuestionEntity = new List<AnswerForQuestionEntity>()
                    
                };
            
                 _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                response.Data = question;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t create question: " + e);
            }

            return response;
        }

        public async Task<Response<IQuestion>> ChangeQusetion(QuestionInfo questionInfo)
        {
            var response=new Response<IQuestion>();

            try
            {
                var question = await _context.Questions
                    .Where(p => p.Id == questionInfo.QuestionId)
                    .FirstOrDefaultAsync();
                if (question==null)
                {
                    response.Error=new Error(404,"Question not found!");
                    return response;
                }

                question.Name = questionInfo.Name;
                question.Description = questionInfo.Description;

                await _context.SaveChangesAsync();

                response.Data = question;
                return response;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t create question: " + e);
            }

            return response;
        }

        public async Task<Response<OperationResultEnum>> DeleteQuestion(int questionId)
        {
           var response=new Response<OperationResultEnum>();

            try
            {
                var question = await _context.Questions
                    .Where(p => p.Id == questionId)
                    .FirstOrDefaultAsync();

                if (question==null)
                {
                    response.Error=new Error(404,"Question not found!");
                    return response;
                }

                 _context.Questions.Remove(question);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t create question: " + e);
            }

            return response;
        }

        public async Task<Response<IQuestionInfo>> ShowQuestion(int questionId)
        {
            var response=new Response<IQuestionInfo>();

            try
            {
                var isQuestion = await _context.Questions.AnyAsync(p => p.Id == questionId);
                if (!isQuestion)
                {
                    response.Error = new Error(404, "Question not found");
                    return response;
                }
                var question = _context.Questions
                    .AsNoTracking()
                    .Where(p => p.Id == questionId)
                    .Include(p=>p.AnswerForQuestionEntity)
                    .ThenInclude(p=>p.Answer)
                    .FirstOrDefault()
                    .AnswerForQuestionEntity
                    .Select(x=>new QuestionInfo(x.Question))
                    .FirstOrDefault();
                

                response.Data = question;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t get question: " + e);
            }

            return response;
        }

        public async Task<Response<ICollection<IQuestionInfo>>> ShowAllQuestions()
        {
            var response=new Response<ICollection<IQuestionInfo>> ();

            try
            {
                var question = await _context.Questions
                    .AsNoTracking()
                    .Include(p => p.AnswerForQuestionEntity)
                    .ThenInclude(p => p.Answer)
                    .ToListAsync();

                var questionList = new List<IQuestionInfo>();

                foreach (var item in question ) {
                    questionList.Add(new QuestionInfo(item));
                }

                response.Data = questionList;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t create question: " + e);
            }

            return response;
        }

        public async Task<Response<ICollection<QuestionInfo>>> ShowAllQuestionsByTestId(int testId)
        {
            var response=new Response<ICollection<QuestionInfo>>();

            try
            {
                var isTest = await _context.Test.AnyAsync(p => p.Id == testId);

                if (!isTest)
                {
                    response.Error = new Error(404, "Test not found!");
                    return response;
                }
                var questions = _context.Test
                    .AsNoTracking()
                    .Where(p => p.Id == testId)
                    .Include(p => p.QuestionForTestEntity)
                    .FirstOrDefault()
                    .QuestionForTestEntity
                    .Select(x => new QuestionInfo(x.Question))
                    .ToList();
                response.Data = questions;
                

            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t show all question by test Id: " + e);
            }

            return response;
        }

        public async Task<Response<OperationResultEnum>> AddQuesttionsToTheTest(int testId, ICollection<QuestionInfo> questionsList)
        {
            var response=new Response<OperationResultEnum>()
            {
                Data= OperationResultEnum.Fail
            };

            try
            {
                var test = await _context.Test.Where(p => p.Id == testId).FirstOrDefaultAsync();
                if (test==null)
                {
                    response.Error=new Error(404,"Test not found! ");
                }

                var questions=new List<QuestionEntity>();
                foreach (var item in questionsList)
                {
                    questions.Add(new QuestionEntity(item.QuestionId,item.Name,item.Description));
                }

                foreach (var item in questions)
                {
                  await  _context.Questions.AddAsync(item);
                  await _context.QuestionsForTest.AddAsync(new QuestionForTestEntity(item, test));
                }

                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;
                return response;



            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t  add question by test Id: " + e);
            }


            return response;
        }

        public async Task<Response<OperationResultEnum>> AddQuesttionsToTheTest(int testId, ICollection<int> questionsID)
        {
            var response = new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };

            try
            {
                var test = await _context.Test.Where(p => p.Id == testId).FirstOrDefaultAsync();
                if (test == null)
                {
                    response.Error = new Error(404, "Test not found! ");
                }

                foreach (var item in questionsID)
                {
                  
                    await _context.QuestionsForTest.AddAsync(new QuestionForTestEntity(item, test));
                }

                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;
                return response;



            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t  add question by test Id: " + e);
            }


            return response;
        }


        public async Task<Response<OperationResultEnum>> DeleteQuestionByTestId(int testId, int questionId)
        {
            var response=new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };

            try
            {
                var question = await _context.QuestionsForTest
                    .Where(p => p.TestId == testId && p.QuestionId == questionId).FirstOrDefaultAsync();
                if (question==null)
                {
                    response.Error=new Error(404,"Question not found! Maby you input an correct fild!");
                    return response;
                }

                _context.QuestionsForTest.Remove(question);
                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;

            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t  add question by test Id: " + e);
            }
            return response;
        }





        public async Task<Response<OperationResultEnum>> AddQuesttionToTheTest(int testId, int questionId)
        {
            var response = new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };

            try
            {
                var test = await _context.Test.Where(p => p.Id == testId).FirstOrDefaultAsync();
                if (test == null)
                {
                    response.Error = new Error(404, "Test not found! ");
                    return response;
                }

                var isQuestionReal = await _context.Questions.AnyAsync(p => p.Id == questionId);
                if (!isQuestionReal)
                {
                    response.Error = new Error(404, "Question not found! ");
                    return response;
                }

                await _context.QuestionsForTest.AddAsync(new QuestionForTestEntity(questionId, test));


                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;
                return response;



            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t  add question by test Id: " + e);
            }

            return response;

        }
    }
    }
