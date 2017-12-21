using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface IAnswerForQuestionEntity
    {
        int Id { get; set; }
        QuestionEntity Question { get; set; }
        AnswerEntity Answer { get; set; }
    }
}
