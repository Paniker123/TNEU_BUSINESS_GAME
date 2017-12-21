using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface IQuestion
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ICollection<AnswerForQuestionEntity> AnswerForQuestionEntity { get; set; }
    }
}
