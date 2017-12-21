using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.TestDTO;

namespace Common.DTO.GameDTO
{
    public class FinishGame
    {
        public int GameId { get; set; }
        public int UserOneId { get; set; }
        public TestInfo TestUser1 { get; set; }
        public int UserTwoId { get; set; }
        public TestInfo TestUser2 { get; set; }
    }
}
