using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces.Entity;

namespace Common.Entity
{
  
    public class QuestionForTestEntity: IQuestionForTestEntity
    {
        public int Id { get; set; }
        [ForeignKey("QuestionId")]
        public QuestionEntity Question { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("TestId")]
        public TestEntity Test { get; set; }

        public int TestId { get; set; }

        public QuestionForTestEntity()
        {
        }

        public QuestionForTestEntity(QuestionEntity question, TestEntity test)
        {
            Question = question;
            Test = test;
        }
        public QuestionForTestEntity(int questionId, TestEntity test)
        {
            QuestionId = questionId;
            Test = test;
        }
    }
}
