using System.Threading.Tasks;
using Common.Interfaces.Entity;
using Common.Socket.Communication;

namespace Common.Socket.Interfaces
{
   public interface ISocketServer
   {
       Task StartGame(Notification<IGame> game);
       Task FinishGame(Notification<IGame> game);
       Task QuestionNotifi(Notification<string> meeageString);
   }
}
