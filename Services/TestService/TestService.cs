using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.TestDTO;
using Common.Entity;
using Common.Enum;
using Common.Interfaces.Entity;
using Common.Interfaces.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;
using Common.Interfaces.DTO;

namespace Services.TestService
{
    public class TestService:ITestService
    {

        private readonly MSContext _context;

        public TestService(MSContext context)
        {
            _context = context;
        }

        public async Task<Response<ITest>> CreateTest(string nameTest)
        {
           var response=new Response<ITest>();

            try
            {
                var test = new TestEntity()
                {
                    Name = nameTest
                };

                await _context.Test.AddAsync(test);
               
                await _context.SaveChangesAsync();
                response.Data = test;

            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t create test: " + e);
            }

            return response;
        }

        public async Task<Response<ITest>> ChangeTest(ChangeTest change)
        {
            var response=new Response<ITest>();

            try
            {
                int id = Convert.ToInt32(change.Id);
                var test = await _context.Test.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (test==null)
                {
                    response.Error=new Error(404,"Test not found!");
                    return response;
                }

                test.Name = change.Name;

              
                await _context.SaveChangesAsync();

                response.Data = test;

            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t shange test: " + e);
            }

            return response;
        }

        public async Task<Response<ITestInfo>> ShowTestById(int testId)
        {
           
            var response=new Response<ITestInfo>();

            try
            {
                var isTestReal = await _context.Test.AnyAsync(p => p.Id == testId);
                if (!isTestReal)
                {
                    response.Error=new Error(404,"Test not found");
                    return response;
                }

                var test = _context.Test
                    .AsNoTracking()
                    .Where(p => p.Id == testId)
                    .Include(p => p.QuestionForTestEntity)
                    .ThenInclude(p=>p.Question)
                    .ThenInclude(p=>p.AnswerForQuestionEntity)
                    .ThenInclude(p=>p.Answer)
                    .FirstOrDefault()
                    .QuestionForTestEntity
                    .Select(x => new TestInfo(x.Test))
                    .FirstOrDefault();



            
                response.Data = test;

        
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t show test by id: " + e);
            }

            return response;
        }

        public async Task<Response<ICollection<ITestInfo>>> ShowAllTests()
        {
            var response=new Response<ICollection<ITestInfo>>();

            try
            {
                var testList = await _context.Test
                    .AsNoTracking()
                    .Include(p => p.QuestionForTestEntity)
                    .ThenInclude(p=>p.Question)
                    .ThenInclude(p=>p.AnswerForQuestionEntity)
                    .ThenInclude(p=>p.Answer)
                    .ToListAsync();
                var tests=new List<ITestInfo>();
                foreach (var item in testList)
                {
                    tests.Add(new TestInfo(item));
                }
                response.Data = tests;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t get all tests: " + e);
            }

            return response;
        }

        public async Task<Response<OperationResultEnum>> DeleteTest(int testId)
        {
            var response=new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };
            try
            {
                var test = await _context.Test.Where(p => p.Id == testId).FirstOrDefaultAsync();
                if (test==null)
                {
                    response.Error=new Error(404,"Test not found!");
                    return response;
                }

                _context.Test.Remove(test);
                await _context.SaveChangesAsync();
                response.Data = OperationResultEnum.Success;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t delete test: " + e);
            }

            return response;
        }

        public async Task<Response<OperationResultEnum>> AddTestToTheGame(int gameId, int testId)
        {
            var response = new Response<OperationResultEnum>() {
                Data = OperationResultEnum.Fail
           };

            try
            {
                var game = await _context.Games.Where(p => p.Id == gameId).FirstOrDefaultAsync();
                if (game==null) {
                    response.Error = new Error(404, "Game not found");
                    return response;
                }
                var test = await _context.Test.Where(p => p.Id == testId).FirstOrDefaultAsync();
                if (test==null) {
                    response.Error = new Error(404,"Test not found");
                    return response;
                }

                await _context.TestsForGame.AddAsync(new TestForGameEntity(test,game));
                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;
                return response;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t add test: " + e);
            }
            

            return response;
        }
    }
}
