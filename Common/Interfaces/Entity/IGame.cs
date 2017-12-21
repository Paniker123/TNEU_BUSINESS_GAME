using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface IGame
    {
        int Id { get; set; }
       
        TestForGameEntity Test { get; set; }
        ICollection<PlayerForGameEntity> Players { get; set; }
        int TotaleScore { get; set; }
        string WinnerPlayer { get; set; }
    }
}
