using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;
using Common.Interfaces.DTO;

namespace Common.DTO.GameDTO
{
    public class GameInfo: IGameDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TestEntity Test { get; set; }
        public ICollection<PlayerForGameEntity> Players { get; set; }
        public int TotaleScore { get; set; }
        public string WinnerPlayer { get; set; }
    }
}
