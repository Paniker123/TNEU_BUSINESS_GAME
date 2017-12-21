using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface ITest
    {
         int Id { get; set; }
         string Name { get; set; }
         ICollection<QuestionForTestEntity> QuestionForTestEntity { get; set; }
    }
}
