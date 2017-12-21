using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Communication;
using Common.DTO.GameDTO;
using Common.DTO.TestDTO;
using Common.Enum;
using Common.Interfaces.DTO;
using Common.Interfaces.Entity;

namespace Common.Interfaces.Services
{
   public interface IGameService
   {
       Task<Response<IGame>> StartGame(StartGame game);
       Task<Response<IGame>> FinishGame(FinishGame gameFinish);
       Task<Response<ICollection<IGame>>> ShowAllGamesStory();
       Task<Response<IGame>> ShowGameResult(int GameId);
   }
}
