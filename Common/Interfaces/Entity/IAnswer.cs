using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Entity
{
   public interface IAnswer
    {
        int Id { get; set; }
        string Name { get; set; }
        bool TrueOrFalse { get; set; }
    }
}
