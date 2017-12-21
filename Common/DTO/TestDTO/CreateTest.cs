using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.DTO.TestDTO
{
    public class CreateTest
    {
        public string Name { get; set; }
        public ICollection<int> QuestionsID { get; set; }
    }
}
