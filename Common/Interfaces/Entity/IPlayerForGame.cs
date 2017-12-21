using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;
using Common.Enum;

namespace Common.Interfaces.Entity
{
   public interface IPlayerForGame
    {
        int Id { get; set; }
        GameRole GameRole { get; set; }
        UserEntity User { get; set; }
        GameEntity Game { get; set; }
    }
}
