using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;
using Common.Interfaces.Entity;

namespace Common.Entity
{

    public class PlayerForGameEntity : IPlayerForGame
    {
        public int Id { get; set; }
        public GameRole GameRole { get; set; }
 
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }

        public string UserId { get; set; }

        [ForeignKey("GameId")]
        public GameEntity Game { get; set; }

        public int GameId { get; set; }

        public PlayerForGameEntity()
        {
        }

        public PlayerForGameEntity(UserEntity user, GameEntity game)
        {
            User = user;
            Game = game;
        }
    }
}
