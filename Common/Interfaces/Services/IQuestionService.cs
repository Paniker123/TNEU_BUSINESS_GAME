using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.QuestionDTO;
using Common.Enum;
using Common.Interfaces.DTO;
using Common.Interfaces.Entity;

namespace Common.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<Response<IQuestion>> CreateQuestion(QuestionDTO createQuestion);
        Task<Response<IQuestion>> ChangeQusetion(QuestionInfo questionInfo);
        Task<Response<OperationResultEnum>> DeleteQuestion(int questionId);
        Task<Response<IQuestionInfo>> ShowQuestion(int questionId);
        Task<Response<ICollection<IQuestionInfo>>> ShowAllQuestions();
        Task<Response<ICollection<QuestionInfo>>> ShowAllQuestionsByTestId(int testId);
        Task<Response<OperationResultEnum>> AddQuesttionsToTheTest(int testId, ICollection<QuestionInfo> questionsList);
        Task<Response<OperationResultEnum>> AddQuesttionsToTheTest(int testId, ICollection<int> questionsList);
        Task<Response<OperationResultEnum>> AddQuesttionToTheTest(int testId, int questionId);
        Task<Response<OperationResultEnum>> DeleteQuestionByTestId(int testId,int questionId);

    }
}
