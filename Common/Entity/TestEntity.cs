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
   
    public class TestEntity:ITest
    {
        
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public ICollection<QuestionForTestEntity> QuestionForTestEntity { get; set; }
            =new List<QuestionForTestEntity>();

        public TestEntity()
        {
        }

        public TestEntity(ITest test)
        {
            Id = test.Id;
            Name = test.Name;
            QuestionForTestEntity = test.QuestionForTestEntity;
        }
    }
}
