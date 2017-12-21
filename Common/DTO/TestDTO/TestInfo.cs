using Common.DTO.QuestionDTO;
using Common.Interfaces.DTO;
using Common.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.TestDTO
{
   public class TestInfo: ITestInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuestionInfo> questionInfo { get; set; } = new List<QuestionInfo>();

        public TestInfo(ITest test)
        {
            Id = test.Id;
            Name = test.Name;
            foreach (var item in test.QuestionForTestEntity) {
                questionInfo.Add(new QuestionInfo(item.Question));
            }
        }
    }
}
