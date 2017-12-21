using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.AnswerDTO
{
    public class ChangeAnswer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrueOrFalse { get; set; }

        public ChangeAnswer()
        {
        }
    }
}
