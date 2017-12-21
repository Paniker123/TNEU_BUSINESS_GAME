using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO.AnswerDTO;
using Common.DTO.Communication;
using Common.Entity;
using Common.Enum;
using Common.Interfaces.Entity;
using Common.Interfaces.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Services.AnswerService
{
    public class AnswerService:IAnswerService
    {
        private readonly MSContext _context;

        public AnswerService(MSContext context)
        {
            _context = context;
        }

        public async Task<Response<IAnswer>> CreateAnswer(CreateAnswer answer)
        {
            var response=new Response<IAnswer>();

            try
            {
            
                var newAnswer = new AnswerEntity()
                {
                    Name = answer.Name,
                    TrueOrFalse = answer.TrueOrFalse
                };

                _context.Answers.Add(newAnswer);

                await _context.SaveChangesAsync();

                response.Data = newAnswer;

            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t register new Answer: " + e);
            }

            return response;
        }

        public async Task<Response<IAnswer>> ChangeAnswer(ChangeAnswer answer)
        {
            var response=new Response<IAnswer>();

            try
            {
                var oldAnswer = await _context.Answers
                    .Where(p => p.Id == answer.Id && p.Name == answer.Name)
                    .FirstOrDefaultAsync();
                if (oldAnswer==null)
                {
                    response.Error=new Error(400,"Can`t find answer!");
                    return response;
                }
                bool tempBool;
                if (answer.TrueOrFalse == "true")
                {
                    tempBool = true;
                }
                else
                {
                    tempBool = false;
                }


                oldAnswer.Name = answer.Name;
                oldAnswer.TrueOrFalse = tempBool;
                await _context.SaveChangesAsync();
                response.Data = oldAnswer;
            }
            catch (Exception ex)
            {
                response.Error = new Error(500, "Can`t change answer: " + ex);
            }
            return response;
        }

        public async Task<Response<IAnswer>> ShowAnswerById(int answerId)
        {
            var response=new Response<IAnswer>();
            try
            {
                var answer = await _context.Answers.Where(p => p.Id == answerId).FirstOrDefaultAsync();
                if (answer==null)
                {
                    response.Error=new Error(404,"Answer not found!");
                    return response;
                }
                response.Data = answer;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t show answer by id: " + e);
            }

            return response;
        }

        public async Task<Response<ICollection<IAnswer>>> ShowAllAnswers()
        {
            var response = new Response<ICollection<IAnswer>>();
            try
            {
                var answer = await _context.Answers.ToListAsync();
                if (answer == null)
                {
                    response.Error = new Error(404, "Answers not found!");
                    return response;
                }
                var answerList=new List<IAnswer>();
                foreach (var item in answer)
                {
                    answerList.Add(item);
                }
                response.Data = answerList;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t show all answers: " + e);
            }
            return response;
        }

      

        public async Task<Response<OperationResultEnum>> DeleteAnswer(int answerId)
        {
            var response = new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };
            try
            {
                var answer = await _context.Answers.Where(p => p.Id == answerId).FirstOrDefaultAsync();
                if (answer == null)
                {
                    response.Error = new Error(404, "Answer not found!");
                    return response;
                }
                var answerForQuestion = await _context.AnswersForQuestion.Where(p => p.Answer.Id == answer.Id).FirstOrDefaultAsync();

                if (answerForQuestion!=null) {
                    _context.AnswersForQuestion.Remove(answerForQuestion);
                }
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t delete answer: " + e);
            }

            return response;
        }

        public async Task<Response<IAnswer>> AddAnswerToTheQuestion(int questionId, int answersId)
        {
            var response = new Response<IAnswer>();
            try
            {
                var question = await _context.Questions.Where(p => p.Id == questionId).FirstOrDefaultAsync();
                if (question == null)
                {
                    response.Error = new Error(404, "Test not found!");
                    return response;
                }

                var amswer = await _context.Answers.Where(p => p.Id == answersId).FirstOrDefaultAsync();
                if (amswer == null)
                {
                    response.Error=new Error(404,"Answer not found!!!");
                    return response;
                }


               
               await _context.AnswersForQuestion.AddAsync(new AnswerForQuestionEntity(question, amswer));

                await _context.SaveChangesAsync();

                response.Data = amswer;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t add already answer: " + e);
            }

            return response;
        }

        public async Task<Response<OperationResultEnum>> DeleteAnswerInTheQuestion(int questionId, int answersId)
        {
            var response=new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };
            try
            {
                var answerInQuestion = await _context.AnswersForQuestion
                    .Where(p => p.Question.Id == questionId && p.Answer.Id == answersId)
                    .FirstOrDefaultAsync();
                if (answerInQuestion==null)
                {
                    response.Error=new Error(404,"Can`t find answer in question or it deteted!");
                    return response;
                }

                 _context.AnswersForQuestion.Remove(answerInQuestion);
                await _context.SaveChangesAsync();
                response.Data = OperationResultEnum.Success;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t add already answer: " + e);
            }

            return response;
        }

        public async Task<Response<ICollection<IAnswer>>> AddAnswerToTheQuestion(int questionId,ICollection<CreateAnswer>  answers)
        {
            var response = new Response<ICollection<IAnswer>>();
            try
            {
                var question = await _context.Questions.Where(p => p.Id == questionId).FirstOrDefaultAsync();
                if (question == null)
                {
                    response.Error = new Error(404, "Test not found!");
                    return response;
                }


                if (answers == null||answers.Count==0)
                {
                    response.Error = new Error(400, "Answers not added!!!");
                    return response;
                }

                var newAnswer = new List<AnswerEntity>();
                foreach (var item in answers)
                {
                    _context.Answers.Add(new AnswerEntity
                    {
                        Name = item.Name,
                        TrueOrFalse = item.TrueOrFalse
                    });
                }
                foreach (var item in newAnswer)
                {
                    _context.AnswersForQuestion.Add(new AnswerForQuestionEntity(question, item));
                }
             

                await _context.SaveChangesAsync();


            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t add answers: " + e);
            }

            return response;
        }
    }
}
