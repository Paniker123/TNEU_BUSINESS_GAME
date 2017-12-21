using Common.DTO.QuestionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.DTO
{
    public interface ITestInfo
    {
        int Id { get; set; }
        string Name { get; set; }
        ICollection<QuestionInfo> questionInfo { get; set; }
    }
}
