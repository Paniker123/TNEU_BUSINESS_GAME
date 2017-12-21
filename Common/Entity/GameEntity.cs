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
    public class GameEntity:IGame
    {   
        public int Id { get; set; }
 
        public TestForGameEntity Test { get; set; }=new TestForGameEntity();
        [Range(2, 2)]
        public ICollection<PlayerForGameEntity> Players { get; set; }=new List<PlayerForGameEntity>();


        public int TotaleScore { get; set; } = 0;

        public string WinnerPlayer { get; set; } = string.Empty;

        public GameEntity()
        {
        }

        public GameEntity(IGame game)
        {
            Id = game.Id;
           
            Test = game.Test;
            Players = game.Players;
            TotaleScore = game.TotaleScore;
            WinnerPlayer = game.WinnerPlayer;
        }
    }
}
