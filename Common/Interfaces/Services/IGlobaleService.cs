using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.AccountDTO;
using Common.DTO.AnswerDTO;
using Common.DTO.Communication;

using Common.DTO.QuestionDTO;
using Common.DTO.TestDTO;
using Common.DTO.TokenDTO;
using Common.Enum;
using Common.Interfaces.Entity;

namespace Common.Interfaces.Services
{
   public interface IGlobaleService
    {

        Task<Response<OperationResultEnum>> CreateAccount(CreateAccount createAccount);
        Task<Response<ClaimsIdentity>> LogIn(LogInAccount logIn);
        Task<Response<IUser>> GetUserInfo(string userId);
        Task<IUser> GetUserByCreds(string userNike, string password, UserRole userRole);
        Task<Response<TokenDTO>> GetToken(ClaimsIdentity identity);
        Task<Response<OperationResultEnum>> LogOut(string userId);


        Task<Response<IAnswer>> CreateAnswer(CreateAnswer answer);
        Task<Response<IAnswer>> ChangeAnswer(AnswerDTO answer);
        Task<Response<IAnswer>> ShowAnswerById(int answerId);
        Task<Response<ICollection<IAnswer>>> ShowAllAnswers();
        Task<Response<OperationResultEnum>> DeleteAnswer(int answerId);
        Task<Response<ICollection<IAnswer>>> AddAnswerAlreadyToTheQuestion(int questionId, int answersId);
        Task<Response<ICollection<IAnswer>>> AddAnswerToTheQuestion(int questionId, CreateAnswer answers);


        Task<Response<IQuestion>> CreateQuestion(QuestionDTO createQuestion);
        Task<Response<IQuestion>> ChangeQusetion(QuestionInfo questionInfo);
        Task<Response<OperationResultEnum>> DeleteQuestion(QuestionInfo questionInfo);
        Task<Response<IQuestion>> ShowAllQuestions();
        Task<Response<ICollection<IQuestion>>> ShowAllQuestionsByTestId(int testId);
        Task<Response<OperationResultEnum>> AddQuesttionsToTheTest(int testId, ICollection<QuestionInfo> questionsList);
        Task<Response<OperationResultEnum>> DeleteQuestionByTestId(int testId, int questionId);




        Task<Response<OperationResultEnum>> CreateTest(CreateTest create);
        Task<Response<OperationResultEnum>> ChangeTest(ChangeTest change);
        Task<Response<ITest>> ShowTestById(string TestId);
        Task<Response<ICollection<ITest>>> ShowAllTests();
        Task<Response<OperationResultEnum>> DeleteTest(string TestId);
        Task<Response<ITest>> AddTestToTheGame(int gameId, int testId);

  
        Task<Response<IGame>> ShowAllGamesStory();
        Task<Response<IGame>> ShowGameResult(string GameId);


    }
}
