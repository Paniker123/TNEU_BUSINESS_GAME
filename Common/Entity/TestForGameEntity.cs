using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces.Entity;

namespace Common.Entity
{

   
    public class TestForGameEntity: ITestForGameEntity
    {
        public int Id { get; set; }
        [ForeignKey("TestId")]
        public TestEntity Test { get; set; }

        public int TestId { get; set; }

        [ForeignKey("GameId")]
        public GameEntity Game { get; set; }

        public int GameId { get; set; }

        public TestForGameEntity()
        {
        }

        public TestForGameEntity( TestEntity test, GameEntity game)
        {
           
            Test = test;
            Game = game;
        }
    }
}
