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
 
    public class AnswerEntity:IAnswer
    {
     
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public bool TrueOrFalse { get; set; }
    }
}
