using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;
using Common.Enum;

namespace Common.DTO.GameDTO
{
   public class StartGame
    {
        public string UserId1 { get; set; }
        private GameRole GameRoleUser1 { get; set; }
        public string UserId2 { get; set; }
        private GameRole GameRoleUser2 { get; set; }
    }
}
