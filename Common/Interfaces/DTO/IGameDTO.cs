using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.DTO
{
    interface IGameDTO
    {
        string Id { get; set; }
        string Name { get; set; }
        TestEntity Test { get; set; }
        ICollection<PlayerForGameEntity> Players { get; set; }
       
        int TotaleScore { get; set; }
        string WinnerPlayer { get; set; }
    }
}
