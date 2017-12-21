using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.AnswerDTO;
using Common.Entity;

namespace Common.Interfaces.DTO
{
    public interface IQuestionInfo
    {
       int QuestionId { get; set; }
       string Name { get; set; }
       string Description { get; set; }
       ICollection<AnswerDTO> Answers { get; set; }
    }
}
