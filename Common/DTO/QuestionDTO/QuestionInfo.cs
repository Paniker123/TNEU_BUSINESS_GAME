

using System.Collections.Generic;
using Common.Interfaces.DTO;
using Common.Interfaces.Entity;
namespace Common.DTO.QuestionDTO
{
    public class QuestionInfo: IQuestionInfo
    {
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AnswerDTO.AnswerDTO> Answers { get; set; }=new List<AnswerDTO.AnswerDTO>();

        public QuestionInfo(IQuestion question)
        {
            QuestionId = question.Id;
            Name = question.Name;
            Description = question.Description;
            foreach (var item in question.AnswerForQuestionEntity)
            {
                Answers.Add(new AnswerDTO.AnswerDTO(item.Answer));
            }
        }
    }
}
