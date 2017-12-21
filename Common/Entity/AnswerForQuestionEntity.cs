using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces.Entity;

namespace Common.Entity
{
  
    public class AnswerForQuestionEntity: IAnswerForQuestionEntity
    {
        public int Id { get; set; }
        [ForeignKey("QuestionId")]
        public QuestionEntity Question { get; set; }
        [ForeignKey("AnswerId")]
        public AnswerEntity Answer { get; set; }

        public AnswerForQuestionEntity()
        {
        }

        public AnswerForQuestionEntity(QuestionEntity question, AnswerEntity answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
