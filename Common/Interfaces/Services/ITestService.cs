using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.TestDTO;
using Common.Enum;
using Common.Interfaces.Entity;
using Common.Interfaces.DTO;

namespace Common.Interfaces.Services
{
    public interface ITestService
    {
        Task<Response<ITest>> CreateTest(string nameTest);
        Task<Response<ITest>> ChangeTest(ChangeTest change);
        Task<Response<ITestInfo>> ShowTestById(int testId);
        Task<Response<ICollection<ITestInfo>>> ShowAllTests();
        Task<Response<OperationResultEnum>> DeleteTest(int testId);
        Task<Response<OperationResultEnum>> AddTestToTheGame(int gameId, int testId);
    }
}
