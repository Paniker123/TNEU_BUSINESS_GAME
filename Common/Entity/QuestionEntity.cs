using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces.Entity;

namespace Common.Entity
{

    public class QuestionEntity:IQuestion
    {
       
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<AnswerForQuestionEntity> AnswerForQuestionEntity { get; set; }=new List<AnswerForQuestionEntity>();
       

        public QuestionEntity()
        {
        }

        public QuestionEntity(IQuestion quessQuestion)
        {
            Id = quessQuestion.Id;
            Name = quessQuestion.Name;
            Description = quessQuestion.Description;
            AnswerForQuestionEntity = quessQuestion.AnswerForQuestionEntity;
        }

        public QuestionEntity(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
