using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface ITestForGameEntity
    {
        int Id { get; set; }
        TestEntity Test { get; set; }
        GameEntity Game { get; set; }
    }
}
