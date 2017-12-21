using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces.Entity;
using Common.Interfaces.Services;

namespace Common.DTO.AnswerDTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TrueOrFalse { get; set; }

        public AnswerDTO(IAnswer answer)
        {
            Id = answer.Id;
            Name = answer.Name;
            TrueOrFalse = answer.TrueOrFalse;
        }

        public AnswerDTO()
        {
        }
    }
}
