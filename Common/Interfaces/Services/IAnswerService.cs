using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.AnswerDTO;
using Common.DTO.Communication;
using Common.Enum;
using Common.Interfaces.Entity;

namespace Common.Interfaces.Services
{
     public interface IAnswerService
     {
         Task<Response<IAnswer>> CreateAnswer(CreateAnswer answer);
         Task<Response<IAnswer>> ChangeAnswer(ChangeAnswer answer);
         Task<Response<IAnswer>> ShowAnswerById(int answerId);
         Task<Response<ICollection<IAnswer>>> ShowAllAnswers();
         Task<Response<OperationResultEnum>> DeleteAnswer(int answerId);
         Task<Response<IAnswer>>AddAnswerToTheQuestion(int questionId, int answersId);
         Task<Response<OperationResultEnum>> DeleteAnswerInTheQuestion(int questionId, int answersId);
         Task<Response<ICollection<IAnswer>>> AddAnswerToTheQuestion(int questionId, ICollection<CreateAnswer> answers);

     }
}
