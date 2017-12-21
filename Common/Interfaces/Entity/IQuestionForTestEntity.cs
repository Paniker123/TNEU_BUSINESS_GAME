using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface IQuestionForTestEntity
    {
        int Id { get; set; }
        QuestionEntity Question { get; set; }
        TestEntity Test { get; set; }
    }
}
