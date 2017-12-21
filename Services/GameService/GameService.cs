using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.AnswerDTO;
using Common.DTO.Communication;
using Common.DTO.GameDTO;
using Common.DTO.QuestionDTO;
using Common.DTO.TestDTO;
using Common.Entity;
using Common.Enum;
using Common.Interfaces.Entity;
using Common.Interfaces.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Services.GameService
{
    public class GameService:IGameService
    {

        private readonly MSContext _context;

        public GameService(MSContext context)
        {
            _context = context;
        }


        public async Task<Response<IGame>> StartGame(StartGame game)
        {
            var response=new Response<IGame>();

            try
            {
                var userOne = await _context.Users.Where(p => p.Id == game.UserId1).FirstOrDefaultAsync();
                if (userOne==null)
                {
                    response.Error=new Error(404,"User One not found");
                    return response;
                }

                var userTwo = await _context.Users.Where(p => p.Id == game.UserId2).FirstOrDefaultAsync();
                if (userTwo == null)
                {
                    response.Error = new Error(404, "User Two not found");
                    return response;
                }

                var newGame = new GameEntity();
                await _context.Games.AddAsync(newGame);
                await _context.SaveChangesAsync();

                var testList = await _context.Test.ToListAsync();
                Random random=new Random();
                int b = testList.Capacity;
                bool temp = true;
                do
                {
                    int tempIndex = random.Next(0, b);
                    var test=await _context.Test.Where(p => p.Id == tempIndex).FirstOrDefaultAsync();
                    if (test!=null)
                    {
                        await _context.TestsForGame.AddAsync(new TestForGameEntity(test, newGame));
                        temp = false;
                    }
                } while (temp);

                await _context.PlayerForGame.AddAsync(new PlayerForGameEntity(userOne, newGame));
                await _context.PlayerForGame.AddAsync(new PlayerForGameEntity(userTwo, newGame));

                await _context.SaveChangesAsync();

                response.Data = newGame;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t start game: " + e);
            }

            return response;
        }


        private int TestRezult(TestInfo defaultTest, TestInfo myTest)
        {
            int score = 0;
            var defaultQuestionList=new List<QuestionInfo>();
            foreach (var item in defaultTest.questionInfo)
            {
                defaultQuestionList.Add(item);
            }

            var myQuestionList=new List<QuestionInfo>();

            foreach (var item in myTest.questionInfo)
            {
                myQuestionList.Add(item);
            }

            var defaultAnswerList=new List<AnswerDTO>();
            foreach (var i in defaultQuestionList)
            {
                foreach (var j in i.Answers)
                {
                    defaultAnswerList.Add(j);
                }
               
            }
            var myAnswerList=new List<AnswerDTO>();
            foreach (var i in myQuestionList)
            {
                foreach (var j in i.Answers)
                {
                    myAnswerList.Add(j);
                }
                
            }
            foreach (var defaultAnswer in defaultAnswerList)
            {
                foreach (var myAnswer in myAnswerList)
                {
                    if (defaultAnswer.Id==myAnswer.Id)
                    {
                        if (defaultAnswer.TrueOrFalse==myAnswer.TrueOrFalse)
                        {
                            score++;
                        }
                    }
                }
                
            }




            return score;
        }


        public async Task<Response<IGame>> FinishGame(FinishGame gameFinish)
        {
            var response=new Response<IGame>();

            try
            {
                var game = await _context.Games.Where(p => p.Id == gameFinish.GameId).FirstOrDefaultAsync();
                if (game==null)
                {
                    response.Error=new Error(404,"Game not found!");
                    return response;
                }

                var gameTest = await _context.Test.Where(p => p.Id == game.Test.Id)
                    .Select(x=>new TestInfo(x))
                    .FirstOrDefaultAsync();
                if (gameTest==null)
                {
                    response.Error=new Error(404,"Game test not found!!!");
                    return response;
                }

                var rezultTest1 = TestRezult(gameTest, gameFinish.TestUser1);
                var rezultTest2 = TestRezult(gameTest, gameFinish.TestUser2);

                //TODO:Insert chek for the UserPLayer Role and make game matriks
                /*
                 * Rentozagarbnik + Rentozagarbnik  => -5;-5
                 * Rentozagarbnik + Altruist => +10;0
                 * Rentozagarbnik + Bezbiletnik => +2.5;-2.5
                 * Altruist +  Altruist=> +2;+2
                 * Altruist + Bezbiletnik=> +1;+6
                 * Bezbiletnik + Bezbiletnik=> +5;+5
                 */
                if (rezultTest1> rezultTest2)
                {
                    game.TotaleScore = rezultTest1;
                    game.WinnerPlayer = "User 1 Winner";
                }
                else if(rezultTest1 < rezultTest2)
                {
                    game.TotaleScore = rezultTest2;
                    game.WinnerPlayer = "User 1 Winner";
                }

                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();

                response.Data = game;
                return response;
            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t finish game: " + e);
            }

            return response;
        }

        public Task<Response<ICollection<IGame>>> ShowAllGamesStory()
        {
            throw new NotImplementedException();
        }

        public Task<Response<IGame>> ShowGameResult(int GameId)
        {
            throw new NotImplementedException();
        }
    }
}
